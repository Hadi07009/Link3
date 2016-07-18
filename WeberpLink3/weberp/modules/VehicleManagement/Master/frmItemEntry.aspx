<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/masMain.master" CodeFile="frmItemEntry.aspx.cs" Inherits="modules_VehicleManagement_Master_frmItemEntry" %>
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
                <asp:Label ID="lblleave" Text="Item Entry" runat="server" />
            </asp:Panel>
                        <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBody" Width="100%" Height="100%">
                <div>
                    <table style="width: 99%; text-align: left">
        <tr>
            <td style="width: 342px">
                &nbsp;</td>
            <td style="width: 145px">
                &nbsp;</td>
            <td style="width: 294px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 342px">
                &nbsp;</td>
            <td style="width: 145px">
                Item Search</td>
            <td style="width: 294px">
                <asp:TextBox ID="txtItemSearch" runat="server" autoComplete="off" 
                                                 Width="275px" AutoPostBack="True" >
                </asp:TextBox>
                 <cc1:autocompleteextender
                                runat="server"                                
                                ID="autoComplete1" 
                                BehaviorID="AutoCompleteEx2"                                
                                TargetControlID="txtItemSearch"
                                ServicePath="~/ClientSide/Inventory/services/InvAutoComplete.asmx"
                                ServiceMethod="GetInvItemList"
                                MinimumPrefixLength="3" 
                                CompletionInterval="100"
                                EnableCaching="false"
                                CompletionSetCount="20"                                 
                                CompletionListCssClass ="autocomplete_completionListElement"                            
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters=","                                                                
                                ShowOnlyCurrentWordInCompletionListItem="true" > 
                </cc1:autocompleteextender> 
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td style="width: 342px">
                &nbsp;</td>
            <td style="width: 145px">
                &nbsp;</td>
            <td align="center" style="width: 294px">
                <asp:Button ID="btnItmRpt" runat="server" Text="Item Listing" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnItmRptGrp" runat="server" Text="Item Group" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

                </div>
                <table style="width: 99%; text-align: left">
        <tr>
            <td style="width: 344px; height: 22px;">
                </td>
            <td style="width: 143px; height: 22px;">
                Item Name</td>
            <td style="width: 293px; height: 22px;">
                <asp:TextBox ID="txtItemName" runat="server" Width="275px"></asp:TextBox>
            </td>
            <td style="height: 22px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtItemName" ErrorMessage="Enter Item Name" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Item Code</td>
            <td style="width: 293px">
                <asp:TextBox ID="txtItemCode" runat="server" height="18px" width="275px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtItemCode" ErrorMessage="Enter Item Code" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Secondary Code</td>
            <td style="width: 293px">
                <asp:TextBox ID="txtItemSecCode" runat="server" height="18px" width="275px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtItemSecCode" 
                    ErrorMessage="Enter Item Secondary Code" ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Stock Unit</td>
            <td style="width: 293px">
                <asp:DropDownList ID="cboStkUnit" runat="server" height="18px" width="275px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Purchase Unit</td>
            <td style="width: 293px">
                <asp:DropDownList ID="cboPurUnit" runat="server" height="18px" width="275px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                BOM Flag</td>
            <td style="width: 293px">
                <asp:DropDownList ID="cboBomFlag" runat="server" height="18px" width="275px">
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Account Code</td>
            <td style="width: 293px">
                <asp:DropDownList ID="ddlAccCode" runat="server" Width="275px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Item Type</td>
            <td style="width: 293px">
                <asp:DropDownList ID="cboItemType" runat="server" height="18px" width="275px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                ABC Category</td>
            <td style="width: 293px">
                <asp:DropDownList ID="cboAbcCategory" runat="server" height="18px" 
                    width="275px">
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                    <asp:ListItem>C</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                1 st Group</td>
            <td style="width: 293px">
                <asp:DropDownList ID="cboFirstGrp" runat="server" height="18px" 
                    width="275px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="cboFirstGrp" ErrorMessage="Enter Item 1 st Group" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                2 nd Group</td>
            <td style="width: 293px">
                <asp:DropDownList ID="cboSecondGrp" runat="server" height="18px" 
                    width="275px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="cboSecondGrp" ErrorMessage="Enter Item Second Group" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                3 rd Group</td>
            <td style="width: 293px">
                <asp:DropDownList ID="cboThirdGrp" runat="server" height="18px" 
                    width="275px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="cboThirdGrp" ErrorMessage="Enter Item Third Group" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                4 th Group</td>
            <td style="width: 293px">
                <asp:DropDownList ID="cboFourthGrp" runat="server" height="18px" 
                    width="275px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="cboFourthGrp" ErrorMessage="Enter Item Fourth Group" 
                    ValidationGroup="grpItm"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Maximum Level</td>
            <td style="width: 293px">
                <asp:TextBox ID="txtMaxLevel" runat="server" height="18px" width="275px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Reorder Level</td>
            <td style="width: 293px">
                <asp:TextBox ID="txtReordLevel" runat="server" height="18px" width="275px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Minimum Level</td>
            <td style="width: 293px">
                <asp:TextBox ID="txtMinLevel" runat="server" height="18px" width="275px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Remarks</td>
            <td style="width: 293px">
                <asp:TextBox ID="txtRemarks" runat="server" height="55px" TextMode="MultiLine" 
                    width="275px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Serial Require</td>
            <td style="width: 293px">
                <asp:CheckBox ID="chkSerial" runat="server" Text="Yes" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                Status</td>
            <td style="width: 293px">
                <asp:CheckBox ID="chkStatus" runat="server" Text="Active" 
                     />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td style="width: 143px">
                &nbsp;</td>
            <td style="width: 293px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 344px">
                &nbsp;</td>
            <td align="center" colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" ValidationGroup="grpItm" />
&nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" CausesValidation="False" Text="Clear" Width="70px" />
            </td>
            <td>
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
