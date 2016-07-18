<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masMain.master" CodeFile="AssetCategorization.aspx.cs" Inherits="modules_FixedAsset_Setup_AssetCategorization" %>


<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="ccmsg" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <ccmsg:MessageBox ID="MessageBox1" runat="server" />
    <ccmsg:ConfirmBox ID="ConfirmBox1" runat="server" />    

    <asp:UpdatePanel ID= "upd" runat="server">
    <ContentTemplate>

    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="ITEM CATEGORIZATION" runat="server" />
    </asp:Panel>
    <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="900px">
        <table style="width: 99%; text-align:left">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label33" runat="server" Text="Item Categorization"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" style="height:auto" valign="top">
                                    <div style="text-align:left;padding-top:1px; height:720px; width:420px" >
                                    <asp:Panel ID="Panel1" runat="server" Height="100%" ScrollBars="Auto" Width="100%">
                                        <asp:TreeView ID="TreeViewAssetCategory" runat="server" OnSelectedNodeChanged="TreeViewAssetCategory_SelectedNodeChanged" OnTreeNodePopulate="TreeViewAssetCategory_TreeNodePopulate" ShowLines="True" BorderColor="Silver" BorderStyle="Inset">                                           
                                        </asp:TreeView>
                                    </asp:Panel>
                                    </div>
                                </td>
                                <td colspan="2" align="right" style="height:auto" valign="top">
                                    <div style="text-align:right;padding-top:1px;height:550px;width:570px">
                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="100%" >
                                        <table style="width:99%; text-align:left">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Panel ID="PanelForCategory" runat="server">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="width: 125px">
                                                                    <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                                                                </td>
                                                                <td style="width: 9px">:</td>
                                                                <td>
                                                                    <asp:Label ID="lblCategoryName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Panel ID="PanelForParent" runat="server" Width="100%">
                                                        <table style="width:100%;text-align:left">
                                                            <tr>
                                                                <td style="width: 125px">
                                                                    <asp:Label ID="Label1" runat="server" Text="Type"></asp:Label>
                                                                </td>
                                                                <td style="width: 8px" >:</td>
                                                                <td>
                                                                    <asp:Label ID="lblParentText" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblParentValue" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table style="width:99%;">
                                                        <tr>
                                                            <td style="width: 125px">
                                                                <asp:Label ID="Label2" runat="server" Text="Item Name"></asp:Label>
                                                            </td>
                                                            <td>:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtAssetName" runat="server" Width="400px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table style="width:98%;">
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Panel ID="PanelForSub" runat="server">
                                                                    <table style="width:100%;">
                                                                        <tr>
                                                                            <td style="width: 118px">&nbsp;</td>
                                                                            <td style="width: 2px">&nbsp;</td>
                                                                            <td>
                                                                                <asp:CheckBox ID="CheckBoxSub" runat="server" Text="Sub" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 117px">&nbsp;</td>
                                                            <td style="width: 8px">&nbsp;</td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBoxItemCreate" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxItemCreate_CheckedChanged" Text="Item Create" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="left">
                                                    <asp:Panel ID="PanelForItemCreate" runat="server" CssClass="cpBodyContent"  Width="99%">
                                                        <table style="width:99%; text-align:left">
                                                            <tr>
                                                                <td style="width: 125px" >
                                                                    <asp:Label ID="Label5" runat="server" Text="Stock Unit"></asp:Label>
                                                                </td>
                                                                <td >:</td>
                                                                <td >
                                                                    <asp:DropDownList ID="cboStkUnit" runat="server" width="405px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="color: #FF0000">*</td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                    <asp:Label ID="Label8" runat="server" Text="Account Code"></asp:Label>
                                                                </td>
                                                                <td >:</td>
                                                                <td >
                                                                    <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" OnTextChanged="txtAccCode_TextChanged" Width="400px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="txtAccCode_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtAccCode">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td style="color: #FF0000">*</td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                    <asp:Label ID="Label9" runat="server" Text="Item Type"></asp:Label>
                                                                </td>
                                                                <td >:</td>
                                                                <td >
                                                                    <asp:DropDownList ID="cboItemType" runat="server" width="405px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="color: #FF0000">*</td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                    <asp:Label ID="Label19" runat="server" Text="Serial Require"></asp:Label>
                                                                </td>
                                                                <td >:</td>
                                                                <td >
                                                                    <asp:CheckBox ID="chkSerial" runat="server" Text="Yes" />
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label21" runat="server" Text="FA A/C"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFaAcc" runat="server" AutoPostBack="True" OnTextChanged="txtFaAcc_TextChanged" Width="400px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="txtFaAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtFaAcc">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label22" runat="server" Text="Depreciation A/C"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDepAcc" runat="server" AutoPostBack="True" OnTextChanged="txtDepAcc_TextChanged" Width="400px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="txtDepAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtDepAcc">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label26" runat="server" Text="Accumulated Depreciation A/C"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAcmDepAcc" runat="server" AutoPostBack="True" OnTextChanged="txtAcmDepAcc_TextChanged" Width="400px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="txtAcmDepAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtAcmDepAcc">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label27" runat="server" Text="Disposal A/C"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDispAcc" runat="server" AutoPostBack="True" OnTextChanged="txtDispAcc_TextChanged" Width="400px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="txtDispAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtDispAcc">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label28" runat="server" Text="Revaluation A/C"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtRevAcc" runat="server" AutoPostBack="True" OnTextChanged="txtRevAcc_TextChanged" Width="400px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="txtRevAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" TargetControlID="txtRevAcc">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                             <tr>
                                                                <td>
                                                                    <asp:Label ID="Label30" runat="server" Text="Cogs A/C"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCogsAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtCogsAcc_TextChanged"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="txtCogsAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx"  TargetControlID="txtCogsAcc">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label31" runat="server" Text="Expense A/C"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtExpenseAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtExpenseAcc_TextChanged"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="txtExpenseAcc_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetAccountCode" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx"  TargetControlID="txtExpenseAcc">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label29" runat="server" Text="Model Number"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtModelNumber" runat="server" OnTextChanged="txtModelNumber_TextChanged" Width="400px"></asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label32" runat="server" Text="Useful Life Cycle"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLifeCycle" runat="server" onkeypress="return isNumberKey(event)" OnTextChanged="txtLifeCycle_TextChanged" Width="400px"></asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" >
                                                    <table style="width:99%;">
                                                        <tr>
                                                            <td style="width: 120px">&nbsp;</td>
                                                            <td style="width: 7px">&nbsp;</td>
                                                            <td>
                                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Create New" Width="100px" />
                                                                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" Width="100px" />
                                                            </td>
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
                                                <td >&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                        </div>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="2">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
    </asp:Panel>


     </ContentTemplate> 
 
      <Triggers>  
     
       </Triggers>
       
      </asp:UpdatePanel> 
</asp:Content>
