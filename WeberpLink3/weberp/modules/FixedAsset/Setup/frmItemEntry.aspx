<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmItemEntry.aspx.cs" Inherits="modules_FixedAsset_Setup_frmItemEntry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="Item Entry" runat="server" />
    </asp:Panel>
    <%--<div>--%>
    <table style="width:100%; text-align:left">
        
        <tr>
            <td style="width: 193px"  >
                &nbsp;</td>
            <td style="width: 14px"  >
                &nbsp;</td>
            <td style="width: 410px"   >
                &nbsp;</td>
            <td style="text-align:left">
                &nbsp;</td>
        </tr>
        
        <tr>
            <td style="width: 193px"  >
                <asp:Label ID="Label1" runat="server" Text="Item Search"></asp:Label>
            </td>
            <td style="width: 14px"  >
                :</td>
            <td style="width: 410px"   >
                <asp:TextBox ID="txtItemSearch" runat="server"  
                                                 Width="400px" AutoPostBack="True" ></asp:TextBox>
                 <cc1:AutoCompleteExtender ID="txtItemSearch_AutoCompleteExtender"
                      runat="server" DelimiterCharacters="" 
                     Enabled="True" 
                     ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                     MinimumPrefixLength="1"                      
                     ServiceMethod="GetInvItem"  
                     TargetControlID="txtItemSearch">
                </cc1:AutoCompleteExtender>
            </td>
            <td style="text-align:left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 193px"  >
                &nbsp;</td>
            <td style="width: 14px"  >
                &nbsp;</td>
            <td style="width: 410px"   >
                <asp:Button ID="btnItmRpt" runat="server" 
                    onclick="btnItmRpt_Click" Text="Item Listing" Width="100px" />
                &nbsp;
                <asp:Button ID="btnItmRptGrp" runat="server" 
                    onclick="btnItmRptGrp_Click" Text="Item Group" Width="100px" />
                &nbsp;
                <asp:Button ID="btnSearch" runat="server" CausesValidation="False" onclick="btnSearch_Click" Text="Search" Width="100px" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

<%--</div>--%>
    <table style="width:100%;text-align:left">
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label2" runat="server" Text="Item Name"></asp:Label>
                </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtItemName" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td >
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtItemName" ErrorMessage="Enter Item Name" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label3" runat="server" Text="Item Code"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtItemCode" runat="server" width="400px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtItemCode" ErrorMessage="Enter Item Code" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label4" runat="server" Text="Secondary Code"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtItemSecCode" runat="server" width="400px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtItemSecCode" 
                    ErrorMessage="Enter Item Secondary Code" ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label5" runat="server" Text="Stock Unit"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:DropDownList ID="cboStkUnit" runat="server" width="405px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label6" runat="server" Text="Purchase Unit"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:DropDownList ID="cboPurUnit" runat="server" width="405px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label7" runat="server" Text="BOM Flag"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:DropDownList ID="cboBomFlag" runat="server" width="405px">
                    <asp:ListItem Selected="True"></asp:ListItem>
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label8" runat="server" Text="Account Code"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtAccCode" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtAccCode_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtAccCode_AutoCompleteExtender" 
                    runat="server" 
                    DelimiterCharacters="" 
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx"
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode" 
                    TargetControlID="txtAccCode">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label9" runat="server" Text="Item Type"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:DropDownList ID="cboItemType" runat="server" width="405px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label10" runat="server" Text="ABC Category"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:DropDownList ID="cboAbcCategory" runat="server" 
                    width="405px">
                    <asp:ListItem Selected="True"></asp:ListItem>
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                    <asp:ListItem>C</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label11" runat="server" Text="1 st Group"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:DropDownList ID="cboFirstGrp" runat="server" 
                    width="405px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="cboFirstGrp" ErrorMessage="Enter Item 1 st Group" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label12" runat="server" Text="2 nd Group"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:DropDownList ID="cboSecondGrp" runat="server" 
                    width="405px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="cboSecondGrp" ErrorMessage="Enter Item Second Group" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label13" runat="server" Text="3 rd Group"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:DropDownList ID="cboThirdGrp" runat="server" 
                    width="405px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="cboThirdGrp" ErrorMessage="Enter Item Third Group" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label14" runat="server" Text="4 th Group"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:DropDownList ID="cboFourthGrp" runat="server" 
                    width="405px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="cboFourthGrp" ErrorMessage="Enter Item Fourth Group" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label15" runat="server" Text="Maximum Level"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtMaxLevel" runat="server" width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label16" runat="server" Text="Reorder Level"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtReordLevel" runat="server" width="400px"></asp:TextBox>
            </td>
            <td >
                </td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label17" runat="server" Text="Minimum Level"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtMinLevel" runat="server" width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtRemarks" runat="server" height="55px" TextMode="MultiLine" 
                    width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label19" runat="server" Text="Serial Require"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:CheckBox ID="chkSerial" runat="server" Text="Yes" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label20" runat="server" Text="Status"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:CheckBox ID="chkStatus" runat="server" Text="Active" 
                     />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label21" runat="server" Text="FA A/C"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtFaAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtFaAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtFaAcc_AutoCompleteExtender" runat="server" 
                    DelimiterCharacters="" 
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode" 
                    TargetControlID="txtFaAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label22" runat="server" Text="Depreciation A/C"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtDepAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtDepAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtDepAcc_AutoCompleteExtender" 
                    runat="server" DelimiterCharacters="" 
                    Enabled="True" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode"
                    TargetControlID="txtDepAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
             <td style="width: 192px" >
                <asp:Label ID="Label26" runat="server" Text="Accumulated Depreciation A/C"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtAcmDepAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtAcmDepAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtAcmDepAcc_AutoCompleteExtender" 
                    runat="server" DelimiterCharacters="" 
                    Enabled="True" ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode"
                    TargetControlID="txtAcmDepAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label27" runat="server" Text="Disposal A/C"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtDispAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtDispAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtDispAcc_AutoCompleteExtender" 
                    runat="server" DelimiterCharacters="" 
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx"
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode"
                    TargetControlID="txtDispAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label28" runat="server" Text="Revaluation A/C"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtRevAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtRevAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtRevAcc_AutoCompleteExtender"
                     runat="server" 
                    DelimiterCharacters="" 
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode"
                    TargetControlID="txtRevAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label29" runat="server" Text="Model Number"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtModelNumber" runat="server" Width="400px" OnTextChanged="txtModelNumber_TextChanged"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label30" runat="server" Text="Useful Life Cycle"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtLifeCycle" runat="server" onkeypress="return isNumberKey(event)" Width="400px" OnTextChanged="txtLifeCycle_TextChanged"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label31" runat="server" Text="VAT Account Code"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtVatAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtVatAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtVatAcc_AutoCompleteExtender"
                    runat="server"
                    DelimiterCharacters="" 
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx"
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode"
                    TargetControlID="txtVatAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                <asp:Label ID="Label32" runat="server" Text="TAX Account code"></asp:Label>
            </td>
            <td style="width: 14px" >
                :</td>
            <td style="width: 216px" >
                <asp:TextBox ID="txtTaxAcc" runat="server" Width="400px" AutoPostBack="True" OnTextChanged="txtTaxAcc_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtTaxAcc_AutoCompleteExtender"
                     runat="server"
                    DelimiterCharacters=""
                    Enabled="True" 
                    ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                    MinimumPrefixLength="1" 
                    ServiceMethod="GetAccountCode"
                    TargetControlID="txtTaxAcc">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                &nbsp;</td>
            <td style="width: 14px" >
                &nbsp;</td>
            <td style="width: 216px" >
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 192px" >
                &nbsp;</td>
            <td style="width: 14px" >
                &nbsp;</td>
            <td style="width: 216px" >
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                    onclick="btnSave_Click" ValidationGroup="grpItm" />
                 &nbsp;
                <asp:Button ID="btnClear" runat="server" CausesValidation="False" 
                    onclick="btnClear_Click" Text="Clear" Width="100px" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>   
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers> 
        </asp:UpdatePanel>
    <script src="../../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

