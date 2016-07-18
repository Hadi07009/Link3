<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_Movement_Report.aspx.cs" Inherits="modules_HRMS_Details_frm_Movement_Report" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <style type="text/css">
        .cpHeader {
            color: white;
            background-color: #719DDB;
            font: bold 11px auto "Trebuchet MS", Verdana;
            font-size: 12px;
            cursor: pointer;
            height: 18px;
            padding: 4px;
        }

        .cpBody {
            background-color: #DCE4F9;
            font: normal 12px auto "Trebuchet MS";
            border: 1px gray;
            padding: 4px;
            padding-top: 2px;
            height: 0px;
            overflow: hidden;
        }

        .auto-style4 {
            height: 35px;
        }

        .auto-style5 {
            width: 164px;
        }

        .auto-style6 {
            height: 35px;
            width: 164px;
        }
    </style>
    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        MOVEMENT REPORT
    </asp:Panel>
    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
        <table style="width: 99%; text-align: left">
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label1" runat="server" Text="Select Company"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlcompany" runat="server" CssClass="tbl" Width="375px"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label2" runat="server" Text="Office Location"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlOfficeLocation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOfficeLocation_SelectedIndexChanged" Width="375px">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label3" runat="server" Text="Department"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlDepartmentId" runat="server" AutoPostBack="True" Width="375px">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label4" runat="server" Text="Date From"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <ew:CalendarPopup ID="txtFromDate" runat="server" AutoPostBack="True" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="100px">
                        <MonthHeaderStyle BackColor="#2A2965" />
                        <ButtonStyle CssClass="btn2" />
                    </ew:CalendarPopup>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label5" runat="server" Text="Date To"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <ew:CalendarPopup ID="txtToDate" runat="server" AutoPostBack="True" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="100px">
                        <MonthHeaderStyle BackColor="#2A2965" />
                        <ButtonStyle CssClass="btn2" />
                    </ew:CalendarPopup>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlShift" runat="server" AutoPostBack="True" Visible="False" Width="180px">
                        <asp:ListItem Value="1">Morning</asp:ListItem>
                        <asp:ListItem Value="2">Evening</asp:ListItem>
                        <asp:ListItem Value="3">Night</asp:ListItem>
                        <asp:ListItem Value="4">Weekend</asp:ListItem>
                        <asp:ListItem Value="5">Gen</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">In Time Missing</asp:ListItem>
                        <asp:ListItem>Out Time Missing</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnPreviewSummary" runat="server" CssClass="btn2" OnClick="btnPreviewSummary_Click" Text="Preview Data" Width="160px" />
                    &nbsp;
                                         <asp:Button ID="btnReport" runat="server" CssClass="btn2" OnClick="btnReport_Click" Text="Preview Report" Width="160px" />
                    &nbsp;
                                         <asp:Button ID="btnExport" runat="server" CssClass="btn2" OnClick="btnExport_Click" Text="Export" Width="160px" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="grdAttendanceRecord" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DisplayIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="EmpID" HeaderText="Employee ID" />
                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                            <asp:BoundField DataField="office" HeaderText="Office Location" />
                            <asp:BoundField DataField="Dept" HeaderText="Department" />
                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                            <asp:BoundField DataField="Atnd_det_date" HeaderText="Date" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Atnd_det_intime" HeaderText="In Time" />
                            <asp:BoundField DataField="Atnd_det_outtime" HeaderText="Out Time" />
                            <asp:BoundField DataField="Atnd_det_rmks" HeaderText="Remarks" />

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Individual Movement Report"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label7" runat="server" Text="Employee ID"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtEmpId" runat="server" CssClass="btn2" Width="350px" AutoCompleteType="None"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="txtEmpId_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                        CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionListItemCssClass="autocomplete_listItem2"
                        MinimumPrefixLength="1" ServiceMethod="GetEmpId"
                        ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmpId">
                    </ajaxToolkit:AutoCompleteExtender>

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style4"></td>
                <td class="auto-style4">
                    <asp:Button ID="btnPreviewMovement" runat="server" CssClass="btn2" OnClick="btnPreviewMovement_Click" Text="Preview Data" Width="160px" />
                    &nbsp;<asp:Button ID="btnExport1" runat="server" CssClass="btn2" OnClick="btnExport1_Click" Text="Export" Width="160px" />
                    &nbsp;<asp:Button ID="btnPreviewIndividual" runat="server" CssClass="btn2" OnClick="btnPreviewIndividual_Click" Text="Preview Report" Width="160px" />
                </td>
                <td class="auto-style4"></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="grdMovementRecords" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DisplayIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpID" HeaderText="Employee ID" />
                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                            <asp:BoundField DataField="office" HeaderText="Office Location" />
                            <asp:BoundField DataField="Dept" HeaderText="Department" />
                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                            <asp:BoundField DataField="CheckDate" DataFormatString="{0:d}" HeaderText="Date" />
                            <asp:BoundField DataField="movTime" HeaderText="Time" />

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="PanelLeaveHdr0" runat="server" CssClass="cpHeaderContent" Width="100%" HorizontalAlign="Center">
                        IN AND OUT PUNCH MISSING TIME LIMITATION SETUP
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label8" runat="server" Text="In Punch upto"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <MKB:TimeSelector ID="timeoffIntime" runat="server" AmPm="AM" DisplaySeconds="False" Hour="9">
                    </MKB:TimeSelector>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label9" runat="server" Text="Outpunch Start from"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <MKB:TimeSelector ID="timeoffOuttime" runat="server" AmPm="PM" DisplaySeconds="False" Hour="6">
                    </MKB:TimeSelector>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnExport0" runat="server" CssClass="btn2" OnClick="btnExport0_Click" Text="Save Time" Width="160px" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="PanelLeaveHdr1" runat="server" CssClass="cpHeaderContent" Width="100%">
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:GridView ID="gdvView" runat="server">
                    </asp:GridView>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>


</asp:Content>
