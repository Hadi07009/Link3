<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="frmUOM.aspx.cs" Inherits="modules_FixedAsset_Setup_frmUOM" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Item Unit Setup" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="900px" ScrollBars="Auto">
               <table width="100%" style="text-align:left">
    <tr>
    <td align="left" style="width: 103px">

        &nbsp;</td>
    <td align="left" style="width: 3px">

        &nbsp;</td>
    <td align="left" style="width: 216px">

        &nbsp;</td>
    <td align="left" style="width: 325px">

        &nbsp;</td>
    <td align="left">

        &nbsp;</td>
    </tr>
    <tr>
    <td align="left" style="width: 103px">

        <asp:Label ID="Label1" runat="server" Text="UOM Code"></asp:Label>
        </td>
    <td align="left" style="width: 3px">

        :</td>
    <td align="left" style="width: 216px">

        <asp:TextBox ID="txtUomCode" runat="server" Width="400px"></asp:TextBox>

        </td>
    <td align="left" style="width: 325px">

        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtUomCode" ErrorMessage="Enter UOM Code"></asp:RequiredFieldValidator>
        </td>
    <td align="left">

        &nbsp;</td>
    </tr>
    <tr>
    <td style="width: 103px">

        <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
        </td>
    <td align="left" style="width: 3px">

        :</td>
    <td align="left" style="width: 216px">

        <asp:TextBox ID="txtUomName" runat="server" Width="400px"></asp:TextBox>
        </td>
    <td align="left" style="width: 325px">

        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtUomName" ErrorMessage="Enter UOM Description"></asp:RequiredFieldValidator>
        </td>
    <td>

        &nbsp;</td>
    </tr>
    <tr>
    <td style="width: 103px">

        <asp:Label ID="Label3" runat="server" Text="Decimal Places"></asp:Label>
        </td>
    <td align="left" style="width: 3px">

        :</td>
    <td align="left" style="width: 216px;vertical-align:top">

        <asp:TextBox ID="txtUomDecPlaces" runat="server" Width="400px"></asp:TextBox>
    
        <cc1:NumericUpDownExtender ID="txtUomDecPlaces_NumericUpDownExtender" 
            runat="server" Enabled="True" Maximum="10" 
            Minimum="0" RefValues="" ServiceDownMethod="" 
            ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" 
            TargetButtonUpID="" TargetControlID="txtUomDecPlaces" Width="100" >
            
        </cc1:NumericUpDownExtender>
    
        </td>
    <td align="left" style="width: 325px">

        &nbsp;</td>
    <td>

        &nbsp;</td>
    </tr>
    <tr>
    <td style="width: 103px">

        &nbsp;</td>
    <td align="left" style="width: 3px">

        &nbsp;</td>
    <td align="left" style="width: 216px">

        &nbsp;</td>
    <td align="left" style="width: 325px">

        &nbsp;</td>
    <td>

        &nbsp;</td>
    </tr>
                   <tr>
                       <td style="width: 103px">&nbsp;</td>
                       <td align="left" style="width: 3px">&nbsp;</td>
                       <td align="left" style="width: 216px">&nbsp;</td>
                       <td align="left" style="width: 325px">&nbsp;</td>
                       <td>&nbsp;</td>
                   </tr>
                   <tr>
                       <td style="width: 103px">&nbsp;</td>
                       <td align="left" style="width: 3px">&nbsp;</td>
                       <td align="left" style="width: 216px">
                           <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" Width="100px" />
                           &nbsp;
                           <asp:Button ID="btnClear" runat="server" CausesValidation="False" onclick="btnClear_Click" Text="Clear" Width="100px" />
                       </td>
                       <td align="left" style="width: 325px">&nbsp;</td>
                       <td>&nbsp;</td>
                   </tr>
    <tr>
    <td style="width: 103px">

        &nbsp;</td>
    <td align="left" style="width: 3px">

        &nbsp;</td>
    <td align="left" style="width: 216px">

        &nbsp;</td>
    <td align="left" style="width: 325px">

        &nbsp;</td>
    <td>

        &nbsp;</td>
    </tr>
    <tr>
    <td colspan="5" align="left">
    
                <asp:GridView ID="gvUom" runat="server" Width="100%" 
            AutoGenerateColumns="False" onrowdatabound="gvUom_RowDataBound" 
                    onselectedindexchanged="gvUom_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="Uom_Code" HeaderText="UOM Code">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Uom_Name" HeaderText="UOM Name">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="T_In" HeaderText="Decimal Places">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            <br />
        </td>
    </tr>
    </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
