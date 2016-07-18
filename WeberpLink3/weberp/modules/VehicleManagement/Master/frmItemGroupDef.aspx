<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/masMain.master" CodeFile="frmItemGroupDef.aspx.cs" Inherits="modules_VehicleManagement_Master_frmItemGroupDef" %>

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
                        <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeader" Width="100%">
                <asp:Label ID="lblleave" Text="Item Group Defination" runat="server" />
            </asp:Panel>
                        <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBody" Width="100%" Height="100%">
                            <table style="width: 99%; text-align: left">
        <tr>
            <td style="width: 367px">
                &nbsp;</td>
            <td style="width: 95px">
                &nbsp;</td>
            <td style="width: 40px">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" style="height: 23px; width: 367px">
                &nbsp;</td>
            <td align="right" style="height: 23px; width: 95px">
                Item Groups</td>
            <td align="center" style="height: 23px; width: 40px">
                <asp:DropDownList ID="dlstGrp" runat="server" Width="250px">
                    <asp:ListItem Value="I01">Item Group 01</asp:ListItem>
                    <asp:ListItem Value="I02">Item Group 02</asp:ListItem>
                    <asp:ListItem Value="I03">Item Group 03</asp:ListItem>
                    <asp:ListItem Value="I04">Item Group 04</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="center" class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 367px">
                &nbsp;</td>
            <td align="right" style="width: 95px">
                Group
                Name</td>
            <td style="width: 40px">
                <asp:TextBox ID="txtGrpName" runat="server" Width="250px" height="18px"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtGrpName" ErrorMessage="Enter Group Name"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 367px">
                &nbsp;</td>
            <td align="right" style="width: 95px">
                Short Name</td>
            <td style="width: 40px">
                <asp:TextBox ID="txtGrpSrtName" runat="server" Width="250px" height="18px"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtGrpSrtName" ErrorMessage="Enter Short Name"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 367px">
                &nbsp;</td>
            <td align="right" style="width: 95px">
                &nbsp;</td>
            <td style="width: 40px">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 367px">
                &nbsp;</td>
            <td align="center" colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" 
                    Width="70px" />
                &nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" Width="70px" CausesValidation="False" />
            </td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 367px">
                &nbsp;</td>
            <td align="center" colspan="2">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:GridView ID="gvItemGrpDef" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="441px">
                    
                    <Columns>
                        <asp:BoundField HeaderText="Group Code" DataField="Grp_Def_Id">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Grp_Def_Name" HeaderText="Group Name">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Short Name" DataField="Grp_Def_Short">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width: 367px">
                &nbsp;</td>
            <td style="width: 95px">
                &nbsp;</td>
            <td style="width: 40px">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
    </table>
                            </asp:Panel>
                    </ContentTemplate>
                        <Triggers>
                             <%--<asp:PostBackTrigger ControlID="txtEmpId"/> --%>  
                        </Triggers>
         </asp:UpdatePanel>
</asp:Content>
