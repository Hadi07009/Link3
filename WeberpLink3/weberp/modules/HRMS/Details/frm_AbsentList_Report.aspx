<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_AbsentList_Report.aspx.cs" Inherits="modules_HRMS_Details_frm_AbsentList_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
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
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Absent List Report" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td style="width: 115px">
                            <asp:Label ID="Label1" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="tbl" Width="300px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Date From"></asp:Label>
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
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Date To"></asp:Label>
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
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlDepartmentId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartmentId_SelectedIndexChanged" Width="300px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnShowEmployee" runat="server" CssClass="btn2" OnClick="btnShowEmployee_Click" Text="Pick Department Employe" Visible="False" Width="200px" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Selected Department</asp:ListItem>
                                <asp:ListItem Value="2">ALL Department</asp:ListItem>
                                <asp:ListItem Value="3">SelectedDepartment ALL Date</asp:ListItem>
                            </asp:RadioButtonList>
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
                            <asp:Button ID="btnPreview" runat="server" CssClass="btn2" OnClick="btnPreview_Click" Text="Report Preview" Width="200px" />
                            <asp:Button ID="btnShowAttendance" runat="server" CssClass="btn2" Text="Preview Absent List" Width="200px" OnClick="btnShowAttendance_Click" Visible="False" />
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
                            <asp:GridView ID="grdShiftAllocationPreview" runat="server" AutoGenerateColumns="False" CssClass="myGrid td" EnableModelValidation="True" OnPageIndexChanging="grdShiftAllocationPreview_PageIndexChanging" OnPreRender="grdShiftAllocationPreview_PreRender" OnRowDataBound="grdShiftAllocationPreview_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" />
                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="adate" HeaderText="Date" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkbALL" runat="server" OnCheckedChanged="chkbALL_CheckedChanged" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckIndv" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSubmitAttendance" runat="server" CssClass="btn2" OnClick="btnSubmitAttendance_Click" Text="Approve Attendance" Visible="False" Width="200px" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlShift" runat="server" AutoPostBack="True" Visible="False" Width="180px">
                                <asp:ListItem Value="1">Morning</asp:ListItem>
                                <asp:ListItem Value="2">Evening</asp:ListItem>
                                <asp:ListItem Value="3">Night</asp:ListItem>
                                <asp:ListItem Value="4">Weekend</asp:ListItem>
                                <asp:ListItem Value="5">Gen</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtEmpId" runat="server" CssClass="btn2" Visible="False"></asp:TextBox>
                            <asp:Button ID="btnShowIndividual" runat="server" CssClass="btn2" OnClick="btnShowIndividual_Click" Text="Pick Individual Employe" Visible="False" Width="200px" />
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPreview" />
            <asp:PostBackTrigger ControlID="btnShowAttendance" />
            <asp:PostBackTrigger ControlID="btnSubmitAttendance" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
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
