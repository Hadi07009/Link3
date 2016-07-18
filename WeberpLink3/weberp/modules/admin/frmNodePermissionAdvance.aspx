<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmNodePermissionAdvance.aspx.cs" Inherits="modules_admin_frmNodePermissionAdvance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                            <asp:Label ID="lblleave" Text="Node Permission Advance" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                            <table style="width: 99%; text-align: left">
                            <tr>
                                <td style="width:115px"  >
                                    Select Company</td>
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
                                <td  >
                                    Employee ID</td>
                                <td >
                                    :</td>
                                <td >
                                    <asp:TextBox ID="txtEmpId" runat="server" AutoPostBack="True" autocomplete="off" CssClass="btn2" Width="300px" OnTextChanged="txtEmpId_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender ID="txtEmpId_AutoCompleteExtender" runat="server" TargetControlID="txtEmpId"
                                        BehaviorID ="txtEmpId_AutoCompleteExtender"
                                        MinimumPrefixLength="1"
                                        Enabled="true"
                                        ServiceMethod ="GetEmpId" 
                                        ServicePath="~/modules/Payroll/WebService.asmx"
                                        >
                                    </ajaxToolkit:AutoCompleteExtender>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:GridView ID="grdAssignedNode" runat="server" AutoGenerateColumns="False" Width="50%" OnRowCommand="grdAssignedNode_RowCommand" OnRowDataBound="grdAssignedNode_RowDataBound" OnRowDeleting="grdAssignedNode_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Node ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Node Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNodeName" runat="server" Text='<%# Bind("NodeDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:CommandField HeaderText="Department" ShowSelectButton="True" SelectText="View" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:CommandField>
                                                <asp:CommandField ShowDeleteButton="True" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            <tr>
                                <td  >&nbsp;Add New Node</td>
                                <td >:</td>
                                <td >
                                    <asp:DropDownList ID="ddlNode" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlNode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td  >&nbsp;Department</td>
                                <td >:</td>
                                <td >
                                    <asp:CheckBox ID="CheckBoxForSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxForSelectAll_CheckedChanged" Text="Select All" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td  >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    <asp:CheckBoxList ID="CheckBoxListDepartmentId" runat="server" Width="300px">
                                    </asp:CheckBoxList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td  >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td  >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td  >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn2" Text="Save" Width="200px" OnClick="btnSave_Click" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td  >
                                    &nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td  >
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

                    </ContentTemplate>
                        <Triggers>
                             <asp:PostBackTrigger ControlID="btnSave"/> 
                        </Triggers>
         </asp:UpdatePanel>
</asp:Content>
