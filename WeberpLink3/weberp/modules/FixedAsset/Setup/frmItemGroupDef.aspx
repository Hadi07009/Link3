<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="frmItemGroupDef.aspx.cs" Inherits="modules_FixedAsset_Setup_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Group Setup" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="900px">
               <table style="width: 1114px;text-align:left">
        <tr>
            <td align="center" style="height: 23px; width: 117px">
                &nbsp;</td>
            <td align="right" style="height: 23px; width: 8px">
                &nbsp;</td>
            <td align="center" style="height: 23px; width: 252px">
                &nbsp;</td>
            <td align="center" class="style2">
                &nbsp;</td>
        </tr>
                   <tr>
                       <td style="height: 23px; width: 117px">
                           <asp:Label ID="Label1" runat="server" Text="Item Group"></asp:Label>
                       </td>
                       <td align="left" style="height: 23px; width: 8px">
                           :</td>
                       <td align="center" style="height: 23px; width: 252px">
                           <asp:DropDownList ID="dlstGrp" runat="server" Width="405px">
                               <asp:ListItem Value="I01">Item Group 01</asp:ListItem>
                               <asp:ListItem Value="I02">Item Group 02</asp:ListItem>
                               <asp:ListItem Value="I03">Item Group 03</asp:ListItem>
                               <asp:ListItem Value="I04">Item Group 04</asp:ListItem>
                           </asp:DropDownList>
                       </td>
                       <td align="center" class="style2">&nbsp;</td>
                   </tr>
        <tr>
            <td style="width: 117px">
                <asp:Label ID="Label2" runat="server" Text="Group Name"></asp:Label>
            </td>
            <td align="left" style="width: 8px">
                :</td>
            <td style="width: 252px">
                <asp:TextBox ID="txtGrpName" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtGrpName" ErrorMessage="Enter Group Name"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 117px">
                <asp:Label ID="Label3" runat="server" Text="Group Short  Name"></asp:Label>
            </td>
            <td align="left" style="width: 8px">
                :</td>
            <td style="width: 252px">
                <asp:TextBox ID="txtGrpSrtName" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtGrpSrtName" ErrorMessage="Enter Short Name"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 117px">
                &nbsp;</td>
            <td align="right" style="width: 8px">
                &nbsp;</td>
            <td style="width: 252px;text-align:left" >
                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" Width="100px" />
                &nbsp;
                <asp:Button ID="btnClear" runat="server" CausesValidation="False" onclick="btnClear_Click" Text="Clear" Width="100px" />
            </td>
            <td class="style1">
                &nbsp;</td>
        </tr>
                   <tr>
                       <td style="width: 117px">&nbsp;</td>
                       <td align="right" style="width: 8px">&nbsp;</td>
                       <td style="width: 252px">&nbsp;</td>
                       <td class="style1">&nbsp;</td>
                   </tr>
                   <tr>
                       <td colspan="4">
                           <asp:GridView ID="gvItemGrpDef" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" onrowdatabound="gvItemGrpDef_RowDataBound" onselectedindexchanged="gvItemGrpDef_SelectedIndexChanged" Width="100%">
                               <Columns>
                                   <asp:BoundField DataField="Grp_Def_Id" HeaderStyle-Width="100px" HeaderText="Group Code" ItemStyle-Width="100px" />
                                   <asp:BoundField DataField="Grp_Def_Name" HeaderStyle-Width="300px" HeaderText="Group Name" ItemStyle-Width="300px" />
                                   <asp:BoundField DataField="Grp_Def_Short" HeaderStyle-Width="200px" HeaderText="Short Name" ItemStyle-Width="200px" />
                               </Columns>
                           </asp:GridView>
                       </td>
                   </tr>
    </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
