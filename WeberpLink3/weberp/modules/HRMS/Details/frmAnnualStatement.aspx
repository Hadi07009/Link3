<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmAnnualStatement.aspx.cs" Inherits="modules_HRMS_Details_frmAnnualStatement" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                Annual Statement
            </asp:Panel>


            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 182px; text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" Width="355px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px; text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="From Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px; text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="To Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px; text-align: right">
                            <asp:Label ID="Label99" runat="server" Text="Employee Search"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtemployeeSearch" runat="server" AutoPostBack="True" CssClass="btn2" OnTextChanged="txtemployeeSearch_TextChanged" Width="350px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtemployeeSearch_AutoComplxtender" runat="server" BehaviorID="txtemployeeSearch_Autopxtender" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem2" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtemployeeSearch">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px">&nbsp;</td>
                        <td align="center">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 182px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnView" runat="server" Text="View" Width="100px" OnClick="btnView_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px">
                            <asp:Button ID="btnExporttoExcel" runat="server" OnClick="btnExporttoExcel_Click" Text="Export to Excel" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdAnnualStatement" runat="server" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. no.">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmpName" HeaderText="Name of the employee" />
                                    <asp:BoundField DataField="employeeCode" HeaderText="Employee ID #" />
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                    <asp:BoundField DataField="Dept" HeaderText="Department" />
                                    <asp:BoundField DataField="TIN" HeaderText="Taxpayer's identification number" />
                                    <asp:BoundField DataField="salaryAmount" DataFormatString="{0:n}" HeaderText="Total amount of salary, wages, bonus, annuities, pensions, gratuities, commission, fees or profits in lieu of salary and wages including payments made at or in connection with the termination of the employment and advance of salary, etc." />
                                    <asp:BoundField DataField="houseRent" DataFormatString="{0:n}" HeaderText="House rent" />
                                    <asp:BoundField DataField="conveyanceAmount" DataFormatString="{0:n}" HeaderText="Conveyance" />
                                    <asp:BoundField DataField="entertainmentAmount" DataFormatString="{0:n}" HeaderText="Entertainment" />
                                    <asp:BoundField DataField="medicalAmount" DataFormatString="{0:n}" HeaderText="Medical" />
                                    <asp:BoundField DataField="othersAmount" DataFormatString="{0:n}" HeaderText="Others, if any" />
                                    <asp:BoundField DataField="rentFreeAccommodation" DataFormatString="{0:n}" HeaderText="Value of rent free accommodation or value of any concession in rent for the accommodation provided by the employer" />
                                    <asp:BoundField DataField="freeConveyance" DataFormatString="{0:n}" HeaderText="Value of free conveyance, full time or part time, if any, provided by the employer" />
                                    <asp:BoundField DataField="freeOrConcessionalPassages" DataFormatString="{0:n}" HeaderText="Value of free or concessional passages provided by the employer" />
                                    <asp:BoundField DataField="salaryPaid" DataFormatString="{0:n}" HeaderText="Salary paid by the employer for domestic and personal services to the employer" />
                                    <asp:BoundField DataField="providentFundEmployersContribution" DataFormatString="{0:n}" HeaderText="Employer's contribution to the recognized provident fund / superannuation / pension fund" />
                                    <asp:BoundField DataField="anyBenefit" DataFormatString="{0:n}" HeaderText="Value of any benefit or annuity provided by the employer free of cost or at concessional rate or any other sum not included in the preceding columns" />
                                    <asp:BoundField DataField="liableToTaxAmount" DataFormatString="{0:n}" HeaderText="Total amount liable to tax under section 21 of the Ordinance" />
                                    <asp:BoundField DataField="taxPayableonTheAmount" DataFormatString="{0:n}" HeaderText="Tax payable on the amount in column 13" />
                                    <asp:BoundField DataField="investmentAmount" DataFormatString="{0:n}" HeaderText="Investment, if any, u/s 44(2)(b) of the Ordinance for tax credit" />
                                    <asp:BoundField DataField="taxCreditAmount" DataFormatString="{0:n}" HeaderText="Amount of tax credit" />
                                    <asp:BoundField DataField="taxPayableAmount" DataFormatString="{0:n}" HeaderText="Net amount to tax payable" />
                                    <asp:BoundField DataField="taxDeductedAndPaid" DataFormatString="{0:n}" HeaderText="Tax actually deducted and paid to the Government with challan no. and date" />
                                    <asp:BoundField DataField="remarks" DataFormatString="{0:n}" HeaderText="Remarks" />
                                    <asp:BoundField DataField="totalIncomeSection" DataFormatString="{0:n}" HeaderText="Total income (Taka)-as per section 108" />
                                    <asp:BoundField DataField="totalIncomeSalaryCertificate" DataFormatString="{0:n}" HeaderText="Total income (Taka)-as per salary certificate" />
                                    <asp:BoundField DataField="taxDeductedSalaryCertificate" DataFormatString="{0:n}" HeaderText=" Tax deducted and deposited (Taka)-as per salary certificate" />
                                    <asp:BoundField DataField="diffTotalSalaryIncome" DataFormatString="{0:n}" HeaderText=" Diff-total salary income" />
                                    <asp:BoundField DataField="diffTotalTaxDeposit" DataFormatString="{0:n}" HeaderText=" Diff-total tax deposit" />
                                    <asp:BoundField DataField="diffTotalTaxDeposit(WSSS)" DataFormatString="{0:n}" HeaderText=" Diff-total tax deposit (WS-SS)" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 182px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnView" />
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
