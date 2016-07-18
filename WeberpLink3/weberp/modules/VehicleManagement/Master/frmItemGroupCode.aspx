<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/masMain.master" CodeFile="frmItemGroupCode.aspx.cs" Inherits="modules_VehicleManagement_Master_frmItemGroupCode" %>

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
                <asp:Label ID="lblleave" Text="Item Group Code" runat="server" />
            </asp:Panel>
                        <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBody" Width="100%" Height="100%">
                            <table style="width: 99%; text-align: left">
<tr>
<td align="center" style="width: 14px">

    &nbsp;</td>
<td align="center" style="width: 224px">

    &nbsp;</td>
<td style="width: 125px">

    </td>
<td style="width: 172px" align="left">

    &nbsp;</td>
<td style="width: 247px" align="char">

    &nbsp;</td>
</tr>
<tr>
<td align="center" style="width: 14px">

    &nbsp;</td>
<td align="center" style="width: 224px">

    &nbsp;</td>
<td style="width: 125px">

    Group Name</td>
<td style="width: 172px" align="left">

    <asp:DropDownList ID="dlistItemGroup" Width="250px" runat="server" 
        AutoPostBack="True">
    </asp:DropDownList>

    </td>
<td style="width: 247px" align="char">

    &nbsp;</td>
</tr>
<tr>
<td align="center" style="width: 14px">

    &nbsp;</td>
<td align="center" style="width: 224px">

    &nbsp;</td>
<td style="width: 125px">

    Group Code</td>
<td style="width: 172px" align="left">

    <asp:TextBox ID="txtGroupCode" Width="250px" runat="server" height="18px"></asp:TextBox>
    </td>
<td style="width: 247px" align="char">

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtGroupCode" ErrorMessage="Enter Group Code"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td align="center" style="width: 14px">

    &nbsp;</td>
<td align="center" style="width: 224px">

    &nbsp;</td>
<td style="width: 125px">

    Group Code Name</td>
<td style="width: 172px" align="left">

    <asp:TextBox ID="txtGroupCodeName" Width="250px" runat="server" height="18px"></asp:TextBox>
    </td>
<td style="width: 247px" align="char">

    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtGroupCodeName" ErrorMessage="Group Code Name"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td align="center" style="width: 14px">

    &nbsp;</td>
<td align="center" style="width: 224px">

    &nbsp;</td>
<td style="width: 125px">

    Short Name</td>
<td style="width: 172px" align="left">

    <asp:TextBox ID="txtGroupCodeShortName" Width="250px" runat="server" 
        height="18px"></asp:TextBox>
    </td>
<td style="width: 247px" align="char">

    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtGroupCodeShortName" ErrorMessage="Enter Short Name"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td style="width: 14px">

    &nbsp;</td>
<td style="width: 224px">

    &nbsp;</td>
<td style="width: 125px">

    &nbsp;</td>
<td style="width: 172px">

    &nbsp;</td>
<td style="width: 247px">

    &nbsp;</td>
</tr>
<tr>
<td style="width: 14px">

    &nbsp;</td>
<td style="width: 224px">

    &nbsp;</td>
<td style="width: 125px">

    &nbsp;</td>
<td style="width: 172px">

                <asp:Button ID="btnSave" runat="server" Text="Save" 
                    Width="70px" />
                &nbsp;&nbsp; 
                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="70px" CausesValidation="False" />
    </td>
<td style="width: 247px">

    &nbsp;</td>
</tr>
<tr>
<td style="width: 14px">

    &nbsp;</td>
<td style="width: 224px">

    &nbsp;</td>
<td style="width: 125px">

    &nbsp;</td>
<td style="width: 172px">

                &nbsp;</td>
<td style="width: 247px">

    &nbsp;</td>
</tr>
<tr>
<td colspan="5" align="center">
    <asp:GridView ID="gvItemGroupCode" runat="server" Width="365px" AllowPaging="true" 
                AutoGenerateColumns="False" PageSize="15">
                <Columns>
                    <asp:BoundField DataField="Grp_Code_Id" HeaderText="Group ID" />
                    <asp:BoundField DataField="Grp_Code" HeaderText="Group Code" />
                    <asp:BoundField DataField="Grp_Code_Name" HeaderText="Code Name" />
                    <asp:BoundField DataField="Grp_Code_Sht" HeaderText="Short Name" />
                </Columns>
            </asp:GridView>
    </td>
    </tr>
    </table>
                            </asp:Panel>
                         </ContentTemplate>
                        <Triggers>
                             <%--<asp:PostBackTrigger ControlID="txtEmpId"/> --%>  
                        </Triggers>
         </asp:UpdatePanel>
</asp:Content>
