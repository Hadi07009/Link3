<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/masMain.master" CodeFile="frmUOM.aspx.cs" Inherits="modules_VehicleManagement_Master_frmUOM" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

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
                <asp:Label ID="lblleave" Text="Unit Of Measurement" runat="server" />
            </asp:Panel>
                        <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBody" Width="100%" Height="100%">
                            <table style="width: 99%; text-align: left">
<tr>
<td align="center" style="width: 337px">

    &nbsp;</td>
<td align="center" style="width: 104px">

    &nbsp;</td>
<td align="left" style="width: 204px">

    &nbsp;</td>
<td align="left" style="width: 325px">

    &nbsp;</td>
<td align="center">

    &nbsp;</td>
</tr>
<tr>
<td align="center" style="width: 337px">

    &nbsp;</td>
<td align="left" style="width: 104px">

    UOM Code</td>
<td align="left" style="width: 204px">

    <asp:TextBox ID="txtUomCode" runat="server" Width="180px"></asp:TextBox>

    </td>
<td align="left" style="width: 325px">

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtUomCode" ErrorMessage="Enter UOM Code"></asp:RequiredFieldValidator>
    </td>
<td align="center">

    &nbsp;</td>
</tr>
<tr>
<td style="width: 337px">

    &nbsp;</td>
<td align="left" style="width: 104px">

    Description</td>
<td align="left" style="width: 204px">

    <asp:TextBox ID="txtUomName" runat="server" Width="180px"></asp:TextBox>
    </td>
<td align="left" style="width: 325px">

    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtUomName" ErrorMessage="Enter UOM Description"></asp:RequiredFieldValidator>
    </td>
<td>

    &nbsp;</td>
</tr>
<tr>
<td style="width: 337px">

    &nbsp;</td>
<td align="left" style="width: 104px">

    Decimal Places</td>
<td align="left" style="width: 204px">

    <asp:TextBox ID="txtUomDecPlaces" runat="server" Width="180px"></asp:TextBox>
    
    <cc1:NumericUpDownExtender ID="txtUomDecPlaces_NumericUpDownExtender" 
        runat="server" Enabled="True" Maximum="10" 
        Minimum="0" RefValues="" ServiceDownMethod="" 
        ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" 
        TargetButtonUpID="" TargetControlID="txtUomDecPlaces" Width="100">
    </cc1:NumericUpDownExtender>
    
    </td>
<td align="left" style="width: 325px">

    &nbsp;</td>
<td>

    &nbsp;</td>
</tr>
<tr>
<td style="width: 337px">

    &nbsp;</td>
<td align="left" style="width: 104px">

    &nbsp;</td>
<td align="left" style="width: 204px">

    &nbsp;</td>
<td align="left" style="width: 325px">

    &nbsp;</td>
<td>

    &nbsp;</td>
</tr>
<tr>
<td style="width: 337px">

    &nbsp;</td>
<td align="left" style="width: 104px">

    &nbsp;</td>
<td align="left" style="width: 204px">

    <asp:Button ID="btnSave" Width="90px"  runat="server" Text="Save" 
       />
    &nbsp;&nbsp; 
    <asp:Button ID="btnClear" Width="90px" runat="server" Text="Clear" CausesValidation="False" 
        />
    </td>
<td align="left" style="width: 325px">

    &nbsp;</td>
<td>

    &nbsp;</td>
</tr>
<tr>
<td style="width: 337px">

    &nbsp;</td>
<td align="left" style="width: 104px">

    &nbsp;</td>
<td align="left" style="width: 204px">

    &nbsp;</td>
<td align="left" style="width: 325px">

    &nbsp;</td>
<td>

    &nbsp;</td>
</tr>
<tr>
<td colspan="5" align="center">
    
            <asp:GridView ID="gvUom" runat="server" Width="334px" 
        AutoGenerateColumns="False">
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
