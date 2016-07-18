<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="frmItemTypeSetup.aspx.cs" Inherits="modules_FixedAsset_Setup_frmItemTypeSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Asset Type Setup" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="900px">
               <table width="100%">
    <tr>
    <td align="left" style="width: 117px">

        &nbsp;</td>
    <td style="width: 1px">

        </td>
    <td style="width: 187px" align="left">

        &nbsp;</td>
    <td  align="char">

        &nbsp;</td>
    </tr>
    <tr>
    <td align="left" style="width: 117px">

        <asp:Label ID="Label1" runat="server" Text="Control Head"></asp:Label>
        </td>
    <td style="width: 1px">

        :</td>
    <td style="width: 187px" align="left">

        <asp:DropDownList ID="dlistItemGroup" Width="405px" runat="server" 
            onselectedindexchanged="dlistItemGroup_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>

        </td>
    <td  align="char">

        &nbsp;</td>
    </tr>
                   <tr>
                       <td align="left" style="width: 117px">
                           <asp:Label ID="Label5" runat="server" Text="Asset Type"></asp:Label>
                       </td>
                       <td style="width: 1px">&nbsp;</td>
                       <td align="left" style="width: 187px">
                           <asp:DropDownList ID="ddl1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl1_SelectedIndexChanged" Width="405px">
                           </asp:DropDownList>
                       </td>
                       <td align="char">&nbsp;</td>
                   </tr>
                   <tr>
                       <td align="left" style="width: 117px">
                           <asp:Label ID="Label6" runat="server" Text="Asset Category"></asp:Label>
                       </td>
                       <td style="width: 1px">&nbsp;</td>
                       <td align="left" style="width: 187px">
                           <asp:DropDownList ID="ddl2" runat="server" Width="405px">
                           </asp:DropDownList>
                       </td>
                       <td align="char">&nbsp;</td>
                   </tr>
                   <tr>
                       <td align="left" style="width: 117px">
                           <asp:Label ID="Label3" runat="server" Text="Name"></asp:Label>
                       </td>
                       <td style="width: 1px">:</td>
                       <td align="left" style="width: 187px">
                           <asp:TextBox ID="txtGroupCodeName" runat="server" Width="400px"></asp:TextBox>
                       </td>
                       <td align="char">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGroupCodeName" ErrorMessage="Group Code Name"></asp:RequiredFieldValidator>
                       </td>
                   </tr>
    <tr>
    <td align="left" style="width: 117px">

        <asp:Label ID="Label2" runat="server" Text="Code"></asp:Label>
        </td>
    <td style="width: 1px">

        :</td>
    <td style="width: 187px" align="left">

        <asp:TextBox ID="txtGroupCode" Width="400px" runat="server" Enabled="False"></asp:TextBox>
        </td>
    <td  align="char">

        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtGroupCode" ErrorMessage="Enter Group Code"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
    <td align="left" style="width: 117px">

        <asp:Label ID="Label4" runat="server" Text="Short Name"></asp:Label>
        </td>
    <td style="width: 1px">

        :</td>
    <td style="width: 187px" align="left">

        <asp:TextBox ID="txtGroupCodeShortName" Width="400px" runat="server"></asp:TextBox>
        </td>
    <td  align="char">

        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtGroupCodeShortName" ErrorMessage="Enter Short Name"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
    <td style="width: 117px">

        &nbsp;</td>
    <td style="width: 1px">

        &nbsp;</td>
    <td style=" text-align:left">

        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" Width="100px" />
        &nbsp;
        <asp:Button ID="btnClear" runat="server" CausesValidation="False" onclick="btnClear_Click" Text="Clear" Width="100px" />
        </td>
    <td >

        &nbsp;</td>
    </tr>
                   <tr>
                       <td style="width: 117px">&nbsp;</td>
                       <td style="width: 1px">&nbsp;</td>
                       <td style="width: 187px">&nbsp;</td>
                       <td >&nbsp;</td>
                   </tr>
                   <tr>
                       <td colspan="4" style="text-align:left">
                           <asp:GridView ID="gvItemGroupCode" runat="server" AllowPaging="true" AutoGenerateColumns="False" onpageindexchanging="gvItemGroupCode_PageIndexChanging" onrowdatabound="gvItemGroupCode_RowDataBound" onselectedindexchanged="gvItemGroupCode_SelectedIndexChanged" PageSize="15" Width="99%">
                               <Columns>
                                   <asp:BoundField DataField="Grp_Code_Id" HeaderText="Group ID" />
                                   <asp:BoundField DataField="Grp_Code" HeaderText="Group Code" />
                                   <asp:BoundField DataField="Grp_Code_Name" HeaderText="Code Name" />
                                   <asp:BoundField DataField="Grp_Code_Sht" HeaderText="Short Name" />
                               </Columns>
                           </asp:GridView>
                       </td>
                   </tr>
    <tr>
    <td style="width: 117px">

        &nbsp;</td>
    <td style="width: 1px">

        &nbsp;</td>
    <td style="width: 187px">

                    &nbsp;</td>
    <td >

        &nbsp;</td>
    </tr>
    </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
