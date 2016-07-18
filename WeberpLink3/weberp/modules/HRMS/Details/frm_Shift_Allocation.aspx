<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_Shift_Allocation.aspx.cs" Inherits="modules_HRMS_Details_frm_Shift_Allocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Shift Allocation" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 115px">
                            <asp:Label ID="Label7" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl"
                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="385px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 115px">
                            <asp:Label ID="Label8" runat="server" Text="Date From"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <ew:CalendarPopup ID="txtFromDate" runat="server" AutoPostBack="True" CssClass="txtbox"
                                Culture="English (United Kingdom)" Enabled="true" Width="165px">
                                <MonthHeaderStyle BackColor="#2A2965" />
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 115px">
                            <asp:Label ID="Label10" runat="server" Text="Date To"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <ew:CalendarPopup ID="txtToDate" runat="server" AutoPostBack="True" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="165px">
                                <MonthHeaderStyle BackColor="#2A2965" />
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 115px">
                            <asp:Label ID="Label9" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlDepartmentId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartmentId_SelectedIndexChanged" Width="200px">
                            </asp:DropDownList>
                            <asp:Button ID="btnShowEmployee" runat="server" CssClass="btn2" OnClick="btnShowEmployee_Click" Text="Pick Department Employee" Width="180px" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 115px">
                            &nbsp;</td>
                        <td>:</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 115px">
                            <asp:Label ID="Label11" runat="server" Text="Employee Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmpId" runat="server" CssClass="btn2" Width="200px" AutoPostBack="True" OnTextChanged="txtEmpId_TextChanged"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmpId_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                CompletionListItemCssClass="autocomplete_listItem2"
                                MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmpId">
                            </ajaxToolkit:AutoCompleteExtender>
                            <asp:Button ID="btnShowIndividual" runat="server" CssClass="btn2" OnClick="btnShowIndividual_Click" Text="Pick Individual Employee" Width="180px" />
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
                        <td colspan="2">
                            <table style="width: 100%;">
                                <tr>
                                    <td>&nbsp;<asp:Label ID="lblresult" runat="server" />
                                        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" PopupControlID="pnlpopup" TargetControlID="btnShowPopup">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlpopup" runat="server" BackColor="WhiteSmoke" Height="700px" ScrollBars="Both" Style="display: block" Width="1100px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="4px">
                                            <table style="width: 99%; text-align: left">
                                                <tr>
                                                    <td colspan="5">
                                                        <asp:Panel ID="PanelLeaveHdr0" runat="server" CssClass="cpHeaderContent" Width="100%" HorizontalAlign="Center">
                                                            DEPARTMENT WISE SHIFT ALLOCATION
                                                        </asp:Panel>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Label ID="Label3" runat="server" Text="Employee Details"></asp:Label>
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label17" runat="server" Text="Shift Details"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 78px">
                                                        <asp:Label ID="Label12" runat="server" Text="Department"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="lblShowDept" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                    <td rowspan="4">
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
                                                    <td style="width: 78px">
                                                        <asp:Label ID="Label13" runat="server" Text="Date"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="LabelShowDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 78px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnApply" runat="server" CommandName="Insert" OnClick="btnApply_Click" Text="Apply" Width="80px" CssClass="btn2" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn2" />
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 78px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <asp:GridView ID="GridViewShowEmployeePerDept" runat="server" EnableModelValidation="True" OnRowDataBound="GridViewShowEmployeePerDept_RowDataBound" Width="100%">
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
                                                                        </asp:CheckBoxList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnShowPopup1" runat="server" Style="display: none" />
                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" PopupControlID="pnlpopupemp" TargetControlID="btnShowPopup1">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlpopupemp" runat="server" BackColor="WhiteSmoke" Height="650px" ScrollBars="Both" Style="display: block" Width="1100px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="4px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Panel ID="PanelLeaveHdr1" runat="server" CssClass="cpHeaderContent" Width="100%" HorizontalAlign="Center">
                                                            EMPLOYEE WISE SHIFT ALLOCATION
                                                        </asp:Panel>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td style="width: 121px">&nbsp;<asp:Label ID="Label5" runat="server" Text="Employee Details"></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label18" runat="server" Text="Shift Details"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 121px">
                                                        <asp:Label ID="Label14" runat="server" Text="EmpLoyee ID"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="lblShowEmpId" runat="server"></asp:Label>
                                                    </td>
                                                    <td rowspan="5">
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
                                                    <td style="width: 121px">
                                                        <asp:Label ID="Label15" runat="server" Text="Name"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="lblShowEmp" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 121px">
                                                        <asp:Label ID="Label16" runat="server" Text="Shift Allocate"></asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="lblShowadt" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 121px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 121px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Button ID="btnApplyIndv" runat="server" CommandName="Insert" CssClass="btn2" OnClick="btnApplyIndv_Click" Text="Apply" Width="80px" />
                                                        <asp:Button ID="btnCancel0" runat="server" CssClass="btn2" Text="Cancel" Width="80px" />
                                                    </td>
                                                    <td align="right">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <asp:GridView ID="GridViewShowEmployeeIndividual" runat="server" AutoGenerateColumns="false" EnableModelValidation="True" OnRowDataBound="GridViewShowEmployeeIndividual_RowDataBound" Width="100%">
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
                                                                        </asp:CheckBoxList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="sDate" DataFormatString="{0:d}" HeaderText="Shift Date" ItemStyle-Width="100px">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DayofWeek" HeaderText="Day of Week" ItemStyle-Width="120px">
                                                                    <ItemStyle Width="120px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ShiftID" HeaderText="ID" ItemStyle-Width="50px">
                                                                    <ItemStyle Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Shift" HeaderText="Shift" ItemStyle-Width="50px">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgPf" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdShiftAllocationPreview" runat="server" AutoGenerateColumns="False" CssClass="myGrid td" EnableModelValidation="True" OnPageIndexChanging="grdShiftAllocationPreview_PageIndexChanging" OnPreRender="grdShiftAllocationPreview_PreRender" OnRowDataBound="grdShiftAllocationPreview_RowDataBound" Width="100%">
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsgSal" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
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
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
