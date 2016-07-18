<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmAttendanceTransfer.aspx.cs" Inherits="modules_HRMS_Details_frmAttendanceTransfer" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Attendance Import" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnTransferAttendance" runat="server" CssClass="btn2" OnClick="btnTransferAttendance_Click" Text="Data Import from Text" Visible="False" Width="200px" />
                            <asp:Button ID="btnAttendanceImportFromXl" runat="server" CssClass="btn2" OnClick="btnAttendanceImportFromXl_Click" Text="Data Import From XL " Visible="False" Width="200px" />
                            <asp:FileUpload ID="FileUploadTestFile" runat="server" Visible="False" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblImport" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:110px" >
                            <asp:Label ID="Label7" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" 
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="400px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 110px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:110px">
                            <asp:Label ID="Label1" runat="server" Text="Select Date "></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtFromDate0" runat="server" Width="280px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDate0_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate0">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:110px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:110px">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Text="Import Attendance" Width="280px" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:110px" >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnPostHoliday" runat="server" OnClick="btnPostHoliday_Click" Text="Process Holiday" Width="280px" />
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <ajaxToolkit:AsyncFileUpload ID="AsyncFileUploadForExcel" runat="server" OnClientUploadComplete="AddFileNameToList" OnUploadedComplete="AsyncFileUploadForExcel_UploadedComplete" Width="285px" Visible="False" />
                             <asp:Button ID="btnImportData" runat="server" OnClick="btnImportData_Click" Text="Import Data From Access Database" Visible="False" Width="280px" />
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
                        <td>&nbsp;</td>
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
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                        
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSheetName_SelectedIndexChanged" Width="285px" Visible="False">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:GridView ID="grdGetDataFromExcel" runat="server" Width="100%" EmptyDataText="No Data Found !">
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                           
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnPostAttendanceNew" runat="server" CssClass="btn2" OnClick="btnPostAttendanceNew_Click" 
                                Text="Post Text Attendance By Company" Width="280px" Visible="False" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnPostAttendanceFromXLS" runat="server" CssClass="btn2" OnClick="btnPostAttendanceFromXLS_Click" 
                                Text="Post XL Attendance By Company" Width="280px" Visible="False" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnUpdateData" runat="server" CssClass="btn2" OnClick="btnUpdateData_Click" Text="Post Attendance" Visible="False" Width="150px" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <ew:CalendarPopup ID="txtFromDate" runat="server" AutoPostBack="True" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Visible="False" Width="76px">
                                <MonthHeaderStyle BackColor="#2A2965" />
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                            <ew:CalendarPopup ID="txtToDate" runat="server" AutoPostBack="True" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Visible="False" Width="76px">
                                <MonthHeaderStyle BackColor="#2A2965" />
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
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
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtEmpId" runat="server" CssClass="btn2" Visible="False"></asp:TextBox>
                            <asp:Button ID="btnShowIndividual" runat="server" CssClass="btn2" OnClick="btnShowIndividual_Click" Text="Pick Individual Employe" Width="200px" Visible="False" />
                            <asp:Button ID="btnShowEmployee" runat="server" CssClass="btn2" OnClick="btnShowEmployee_Click" Text="Pick Department Employe" Visible="False" Width="200px" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td></td>
                        <td>
                            <asp:DropDownList ID="ddlDepartmentId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartmentId_SelectedIndexChanged" Visible="False" Width="300px">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnImportLocal" runat="server" OnClick="btnImportLocal_Click" Text="Button" Visible="False" />

                            <asp:FileUpload ID="file_upload" runat="server" class="multi" Visible="False" />

                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <table style="width: 100%;">
                                <tr>
                                    <td width="50px"></td>
                                    <td></td>
                                    <td  colspan="2" width="200px"></td>
                                    <td align="left"  colspan="2">&nbsp; &nbsp;
                                    </td>
                                    <td ></td>
                                </tr>
                                <tr>
                                    <td width="50px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="200px">&nbsp;</td>
                                    <td align="right">&nbsp;&nbsp;</td>
                                    <td></td>
                                    <td align="left">&nbsp;</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;<asp:Label ID="lblresult" runat="server" />
                                        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" 
                                            CancelControlID="btnCancel" PopupControlID="pnlpopup" TargetControlID="btnShowPopup">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlpopup" runat="server" BackColor="#ccccff" Height="550px" ScrollBars="Both" Style="display: block" Width="725px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="center" class="cpHeaderContent" colspan="6">
                                                        <asp:Label ID="Label3" runat="server" Text="Employee Details"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td align="right" rowspan="5">
                                                        <asp:GridView ID="grdLoadShiftType" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:BoundField DataField="Shift Code" HeaderText="Shift Code" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text="="></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                                <asp:BoundField DataField="From" HeaderText="From" />
                                                                <asp:BoundField DataField="To" HeaderText="To" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">Department</td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="lblShowDept" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">Date</td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="LabelShowDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnApply" runat="server" CommandName="Insert" OnClick="btnApply_Click" Text="Apply" Width="80px" CssClass="btn2" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn2" />
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">&nbsp;<asp:GridView ID="GridViewShowEmployeePerDept" runat="server" EnableModelValidation="True" OnRowDataBound="GridViewShowEmployeePerDept_RowDataBound" Width="670px">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkbForSelectALL" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckRet" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shift">
                                                                <ItemTemplate>
                                                                    <asp:CheckBoxList ID="cblForShiftSelect" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">A</asp:ListItem>
                                                                        <asp:ListItem Value="2">B</asp:ListItem>
                                                                        <asp:ListItem Value="3">C</asp:ListItem>
                                                                        <asp:ListItem Value="4">H</asp:ListItem>
                                                                        <asp:ListItem Value="5">G</asp:ListItem>
                                                                        <asp:ListItem Value="6">S</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td></td>
                                                    <td align="right">&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td >&nbsp;</td>
                                    <td >&nbsp;</td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td>&nbsp;<table style="width: 100%;">
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="200px">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td align="left">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td >&nbsp;</td>
                                    <td >&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="50px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="200px">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td align="left">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnShowPopup1" runat="server" Style="display: none" />
                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" PopupControlID="pnlpopupemp" TargetControlID="btnShowPopup1">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlpopupemp" runat="server" BackColor="#ccccff" Height="550px" ScrollBars="Both" Style="display: block" Width="725px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="center" colspan="6">
                                                        <asp:Label ID="Label5" runat="server" Text="Employee Details"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">EmpLoyee ID</td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="lblShowEmpId" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td align="right" rowspan="5">
                                                        <asp:GridView ID="grdLoadShiftType0" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:BoundField DataField="Shift Code" HeaderText="Shift Code" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text="="></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                                <asp:BoundField DataField="From" HeaderText="From" />
                                                                <asp:BoundField DataField="To" HeaderText="To" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">Name</td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="lblShowEmp" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">Shift Allocate</td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="lblShowadt" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td width="50px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Button ID="btnApplyIndv" runat="server" CommandName="Insert" CssClass="btn2" OnClick="btnApplyIndv_Click" Text="Apply" Width="80px" />
                                                        <asp:Button ID="btnCancel0" runat="server" CssClass="btn2" Text="Cancel" Width="80px" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">&nbsp;<asp:GridView ID="GridViewShowEmployeeIndividual" runat="server" EnableModelValidation="True" OnRowDataBound="GridViewShowEmployeeIndividual_RowDataBound" Width="670px" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkbForSelectALL0" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckRetIndv" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shift">
                                                                <ItemTemplate>
                                                                    <asp:CheckBoxList ID="cblForShiftSelectShiftIndv" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">A</asp:ListItem>
                                                                        <asp:ListItem Value="2">B</asp:ListItem>
                                                                        <asp:ListItem Value="3">C</asp:ListItem>
                                                                        <asp:ListItem Value="4">H</asp:ListItem>
                                                                        <asp:ListItem Value="5">G</asp:ListItem>
                                                                        <asp:ListItem Value="6">S</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField ItemStyle-Width="100px" DataField="sDate" HeaderText="Shift Date" DataFormatString="{0:d}">
                                                                <ItemStyle Width="100px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField ItemStyle-Width="120px" DataField="DayofWeek" HeaderText="Day of Week">
                                                                <ItemStyle Width="120px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField ItemStyle-Width="50px" DataField="ShiftID" HeaderText="ID">
                                                                <ItemStyle Width="50px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField ItemStyle-Width="50px" DataField="Shift" HeaderText="Shift">
                                                                <ItemStyle Width="100px"></ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td></td>
                                                    <td align="right">&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td >&nbsp;</td>
                                    <td >&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:GridView ID="GridView1" runat="server">
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgPf" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdShiftAllocationPreview" runat="server" AutoGenerateColumns="False" CssClass="myGrid td" EnableModelValidation="True" OnPageIndexChanging="grdShiftAllocationPreview_PageIndexChanging" OnPreRender="grdShiftAllocationPreview_PreRender" OnRowDataBound="grdShiftAllocationPreview_RowDataBound" Width="100%" Visible="False">
                                <Columns>
                                    <asp:BoundField DataField="Date" HeaderText="Date">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Morning" HeaderText="Morning(A) " />
                                    <asp:BoundField DataField="Evening" HeaderText="Evening(B) " />
                                    <asp:BoundField DataField="Night" HeaderText="Night(C)" />
                                    <asp:BoundField DataField="Weekend" HeaderText="Holiday" />
                                    <asp:BoundField DataField="Gen" HeaderText="General(Gen)" />
                                    <asp:BoundField DataField="Special" HeaderText="Special(S)" />
                                </Columns>
                                <RowStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgSal" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:GridView ID="gdvView" runat="server">
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnTransferAttendance" />
            <asp:PostBackTrigger ControlID="btnAttendanceImportFromXl" />
            <asp:PostBackTrigger ControlID="btnPostAttendanceNew" />
            <asp:PostBackTrigger ControlID="btnPostAttendanceFromXLS" />
            <asp:PostBackTrigger ControlID="btnPostHoliday" />
            <asp:PostBackTrigger ControlID="btnUpdateData" />
            <asp:PostBackTrigger ControlID="btnImportData" />
            <asp:PostBackTrigger ControlID="btnImport" />   
            <asp:PostBackTrigger ControlID="btnPostHoliday" />   
            
                     
        </Triggers>
    </asp:UpdatePanel>

    <script>
        function AddFileNameToList(sender, args) {
            <%=ClientScript.GetPostBackEventReference(updpnl2, "" )%>;
        }
    </script>
</asp:Content>
