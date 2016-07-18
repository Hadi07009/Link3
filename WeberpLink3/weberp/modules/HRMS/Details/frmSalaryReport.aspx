<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmSalaryReport.aspx.cs"  Inherits="modules_HRMS_Details_frmSalaryReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
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
            </style>
                        <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeader" Width="100%">
                            <asp:Label ID="lblleave" Text="Salary Report" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBody" Width="100%" Height="100%">
                         <table style="width: 99%; text-align: left">
                                                         <tr>
                                                             <td>&nbsp;</td>
                                                             <td>&nbsp;</td>
                                                             <td>&nbsp;</td>
                                                         </tr>
                                                         <tr>
                                                             <td align="right">From Date</td>
                                                             <td>:</td>
                                                             <td>
                                                                 <asp:TextBox ID="txtFromDateForStatus" runat="server" Width="178px"></asp:TextBox>
                                                                 <cc1:CalendarExtender ID="txtFromDateForStatus_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDateForStatus">
                                                                 </cc1:CalendarExtender>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td align="right">To Date</td>
                                                             <td>:</td>
                                                             <td>
                                                                 <asp:TextBox ID="txtToDateForStatus" runat="server" Width="178px"></asp:TextBox>
                                                                 <cc1:CalendarExtender ID="txtToDateForStatus_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDateForStatus">
                                                                 </cc1:CalendarExtender>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td>&nbsp;</td>
                                                             <td>&nbsp;</td>
                                                             <td>
                                                                 <asp:Button ID="btnView" runat="server" CssClass="btn2" OnClick="btnView_Click" Text="View" Width="110px" />
                                                                 <asp:Button ID="btnExportAllTypeOfEmp" runat="server" CssClass="btn2" OnClick="btnExportAllTypeOfEmp_Click" Text="Export" Width="110px" />
                                                                 <asp:Button ID="btnReport" runat="server" CssClass="btn2" OnClick="btnReport_Click" Text="Report" Width="110px" />
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td>&nbsp;</td>
                                                             <td>&nbsp;</td>
                                                             <td>&nbsp;</td>
                                                         </tr>
                                                         <tr>
                                                             <td colspan="3">
                                                                 <asp:GridView ID="grdShowAllTypeEmp" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdShowAllTypeEmp_RowDataBound" Width="100%" EmptyDataText="No Data Found ">
                                                                     <Columns>
                                                                         <asp:BoundField DataField="EID" HeaderText="Emp Id" />
                                                                         <asp:BoundField DataField="Employee Name" HeaderText="Name" />
                                                                         <asp:BoundField DataField="Department" HeaderText="Department" />
                                                                         <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                         <asp:BoundField DataField="joiningDate" HeaderText="Joining Date" />
                                                                         <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                         <asp:BoundField DataField="DateStatusType" />
                                                                         <asp:BoundField DataField="Salary" HeaderText="Salary" />
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

                    
</asp:Content>
