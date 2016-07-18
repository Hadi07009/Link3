<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_Attendance_Approval.aspx.cs" Inherits="modules_HRMS_Details_frm_Attendance_Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />  
  <%--  <asp:UpdatePanel ID="updpnl2" runat="server">
                    <ContentTemplate>--%>
                        <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                            <asp:Label ID="lblleave" Text="Attendance Approval" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                             <table style="width: 99%; text-align: left">
                            <tr>
                                <td >
                                    <asp:Label ID="Label7" runat="server" Text="Select Company"></asp:Label>
                                </td>
                                <td >
                                    :</td>
                                <td >
                                    <asp:DropDownList ID="ddlcompany" runat="server" CssClass="tbl" Width="300px" 
                                        AutoPostBack="True" onselectedindexchanged="ddlcompany_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="Label8" runat="server" Text="Date From"></asp:Label>
                                </td>
                                <td >
                                    :</td>
                                <td >
                                    <ew:CalendarPopup ID="txtFromDate" runat="server" AutoPostBack="True" cssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="260px">
                                        <MonthHeaderStyle BackColor="#2A2965" />
                                        <ButtonStyle CssClass="btn2" />
                                    </ew:CalendarPopup>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="Label9" runat="server" Text="Date To"></asp:Label>
                                </td>
                                <td >:</td>
                                <td >
                                    <ew:CalendarPopup ID="txtToDate" runat="server" AutoPostBack="True" cssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="260px">
                                        <MonthHeaderStyle BackColor="#2A2965" />
                                        <ButtonStyle CssClass="btn2" />
                                    </ew:CalendarPopup>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                                 <tr>
                                     <td>
                                         <asp:Label ID="Label12" runat="server" Text="Office Location"></asp:Label>
                                     </td>
                                     <td>:</td>
                                     <td>
                                         <div style="OVERFLOW-Y: scroll; WIDTH: 380px; HEIGHT: 175px; border: 1px solid; border-color: #669999; border-style: Ridge">
                                             <asp:CheckBoxList ID="chkofficelocation" runat="server">
                                             </asp:CheckBoxList>
                                         </div>
                                     </td>
                                     <td>&nbsp;</td>
                                 </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="Label10" runat="server" Text="Department"></asp:Label>
                                </td>
                                <td >:</td>
                                <td >
                                    <asp:DropDownList ID="ddlDepartmentId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartmentId_SelectedIndexChanged" Width="385px">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                                 <tr>
                                     <td>
                                         <asp:Label ID="Label14" runat="server" Text="Category"></asp:Label>
                                     </td>
                                     <td>:</td>
                                     <td>
                                         <asp:DropDownList ID="ddlEmpCategory" runat="server" AutoPostBack="True" Width="385px">
                                             <asp:ListItem Value="-1">ALL</asp:ListItem>
                                             <asp:ListItem>Officer</asp:ListItem>
                                             <asp:ListItem>Staff</asp:ListItem>
                                             <asp:ListItem>Worker</asp:ListItem>
                                         </asp:DropDownList>
                                     </td>
                                     <td>&nbsp;</td>
                                 </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="Label13" runat="server" Text="Employee"></asp:Label>
                                </td>
                                <td >:</td>
                                <td >
                                    <asp:TextBox ID="txtEmployeeSearch" runat="server" AutoPostBack="True" placeholder="Employee Code" Width="380px"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeSearch_AutoCompleteExtender" runat="server" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem2" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeSearch">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    <asp:Button ID="btnShowAttendance" runat="server" CssClass="btn2" Text="Preview Absent List" Width="200px" OnClick="btnShowAttendance_Click" />
                                    <asp:Button ID="btnShowPresentAttendance" runat="server" CssClass="btn2"  Text="Preview Present List" Width="200px" OnClick="btnShowPresentAttendance_Click" />
                                    &nbsp;<asp:Button ID="btnPreview" runat="server" CssClass="btn2" OnClick="btnPreview_Click" Text="Absent List Report Preview" Width="200px" />
                                    &nbsp;<asp:Button ID="btnPreviewPresent" runat="server" CssClass="btn2" OnClick="btnPreviewPresent_Click" Text="Present List Report Preview" Width="200px" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" >
                                    <asp:GridView ID="grdShiftAllocationPreview" runat="server" AutoGenerateColumns="False" CssClass="myGrid td" EnableModelValidation="True" OnPageIndexChanging="grdShiftAllocationPreview_PageIndexChanging" OnPreRender="grdShiftAllocationPreview_PreRender" OnRowDataBound="grdShiftAllocationPreview_RowDataBound" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="adate" DataFormatString="{0:d}" HeaderText="Date" />
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="In time" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <MKB:TimeSelector ID="timeoff1" runat="server" AmPm="AM" DisplaySeconds="False" Hour="9">
                                                    </MKB:TimeSelector>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Out Time" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <MKB:TimeSelector ID="timeoff2" runat="server" AmPm="AM" Date="2013-02-27" DisplaySeconds="False" Hour="9">
                                                    </MKB:TimeSelector>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' TextMode="MultiLine" Width="250px"></asp:TextBox>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkbALL" runat="server" oncheckedchanged="chkbALL_CheckedChanged" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckIndv" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Intime" HeaderText="actin" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">   
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Outtime" HeaderText="actout" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">   
                                                
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                        </Columns>
                                        <RowStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:GridView>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    <asp:Button ID="btnSubmitAttendance" runat="server" CssClass="btn2" OnClick="btnSubmitAttendance_Click" OnClientClick="ShowConfirmBox(this,'Are you sure approve attendance ?'); return false;" Text="Approve Attendance" Visible="False" Width="200px" />
                                    &nbsp;<asp:Button ID="btnDeleteAttendance" runat="server" CssClass="btn2" OnClick="btnDeleteAttendance_Click" OnClientClick="ShowConfirmBox(this,'Are you sure delete attendance ?'); return false;" Text="Delete Attendance" Visible="False" Width="200px" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >
                                    &nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                            </asp:Panel>

<%--                    </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnPreview"/>
                            <asp:PostBackTrigger ControlID="btnShowAttendance"/> 
                            <asp:PostBackTrigger ControlID="btnSubmitAttendance"/>  
                            <asp:PostBackTrigger ControlID="btnPreviewPresent"/>    
                            <asp:PostBackTrigger ControlID="btnDeleteAttendance"/> 
                                                  
                        </Triggers>
         </asp:UpdatePanel>--%>

    <script type = "text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "silver";
                        if (inputList[i].disabled == false) {
                            inputList[i].checked = true;
                        }
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original 
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script> 
</asp:Content>
