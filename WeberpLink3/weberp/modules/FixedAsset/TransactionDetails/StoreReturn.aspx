<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false" CodeFile="StoreReturn.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_StoreReturn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .cpHeader {
            color: white;
            background-color: #719DDB;
            font: bold 11px auto "Trebuchet MS", Verdana;
            font-size: 12px;
            cursor: pointer;
            width: 450px;
            height: 18px;
            padding: 4px;
        }

        .cpBody {
            background-color: #DCE4F9;
            font: normal 11px auto Verdana, Arial;
            border: 1px gray;
            width: 450px;
            padding: 4px;
            padding-top: 2px;
            height: 0px;
            overflow: hidden;
        }
        .auto-style1 {
            width: 129px;
        }
    </style>
    <div align="center">
        <asp:Panel ID="pnlSrchMrrHdr" runat="server" CssClass="cpHeader" Width="100%">
            <div align="center">
                <asp:Label ID="lblSearchMrr" Text="STORE RETURN MEMO " runat="server" />
            </div>

        </asp:Panel>
        <asp:Panel ID="pnlSrchMrrDet" runat="server" CssClass="cpBody" Width="100%" Height="80px">
            <table>
                <tr>
                    <td >&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td style="width: 1px">&nbsp;</td>
                    <td style="width: 525px">
                        <asp:Label ID="lblerrormessage" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" ForeColor="Red"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style=" font-size: 10pt;" class="auto-style1">Search Return Ref</td>
                    <td style="width: 1px">:</td>
                    <td style="width: 525px">
                        <asp:TextBox ID="txtclisearch" runat="server" AutoCompleteType="Disabled" Width="525px"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                            CompletionInterval="100"
                            CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            CompletionListItemCssClass="autocomplete_listItem2"
                            CompletionSetCount="20"
                            DelimiterCharacters=","
                            EnableCaching="false"
                            MinimumPrefixLength="2"
                            ServiceMethod="GetPartyCodeManualyL3T"
                            ServicePath="~/ClientSide/modules/mis/naz/FORMS/HRMS/Forms/WBservices.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true"
                            TargetControlID="txtclisearch">
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td style="width: 1px">&nbsp;</td>
                    <td style="width: 525px">
                        <asp:Button ID="btnshow" runat="server" OnClick="btnshow_Click" Text="Print Data" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td style="width: 1px">&nbsp;</td>
                    <td style="width: 525px">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrchMrr" runat="server"
            TargetControlID="pnlSrchMrrDet"
            CollapseControlID="pnlSrchMrrHdr"
            ExpandControlID="pnlSrchMrrHdr"
            Collapsed="true"
            TextLabelID="lblSearchMrr"
            CollapsedText="STORE RETURN MEMO"
            ExpandedText="STORE RETURN MEMO"
            CollapsedSize="0"
            ExpandedSize="80"
            AutoCollapse="False"
            AutoExpand="False"
            ScrollContents="false"
            ImageControlID="Image1"
            ExpandedImage="~/images/collapse.jpg"
            CollapsedImage="~/images/expand.jpg"
            ExpandDirection="Vertical">
        </cc1:CollapsiblePanelExtender>
    </div>
    <asp:Panel ID="pnlClientInformation" runat="server" CssClass="cpHeader" Width="100%">
        <div align="center"">
            <asp:Label ID="ImageButton5" runat="server" Text="RETURN ITEM" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnl" runat="server" CssClass="pnlNewCSS" Height="500px"  Width="100%">
        <table width="100%">
            <tr>
                <td class="style2" style="width: 189px">&nbsp;</td>
                <td class="style2" style="width: 193px">&nbsp;</td>
                <td class="style2" style="width: 73px">&nbsp;</td>
                <td class="style7" style="width: 215px">&nbsp;</td>
                <td style="width: 121px">&nbsp;</td>
                <td class="style3">&nbsp;</td>
                <td style="width: 259px">&nbsp;</td>
                <td style="width: 385px">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
                <td class="style2" align="center" colspan="6">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="Red"
                        Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                </td>
                <td style="width: 385px">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="8">
                    <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" Width="913px">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 168px">&nbsp;</td>
                                <td style="width: 112px">&nbsp;</td>
                                <td style="width: 1px">&nbsp;</td>
                                <td style="width: 286px">
                                    <asp:Label ID="lblCurrentPeriod" runat="server" Font-Bold="True"
                                        Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 112px;">
                                    <asp:Label ID="Label1" runat="server" Text="Referance"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px;">:</td>
                                <td style="border: 1px solid #CCCCCC;" align="left">
                                    <asp:Label ID="mo" runat="server"></asp:Label>
                                    <asp:Label ID="max" runat="server" Visible="False"></asp:Label>
                                    <%--<asp:Label ID="lbl" runat="server" Visible="False"></asp:Label>--%>
                                    <asp:Label ID="id" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 168px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 112px">
                                    <asp:Label ID="Label2" runat="server" Text="Return Store"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td style="width: 286px">
                                    <asp:DropDownList ID="ddReturnstore" runat="server" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 168px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 112px">
                                    <asp:Label ID="Label4" runat="server" Text="Return Date"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td style="width: 286px">
                                    <asp:TextBox ID="txtreturndate" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy" PopupButtonID="ImageButton1"
                                        TargetControlID="txtreturndate">
                                    </cc1:CalendarExtender>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" Width="16px" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 168px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 112px">Ref No</td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">&nbsp;</td>
                                <td style="width: 286px">
                                    <asp:TextBox ID="txtrefno" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">&nbsp;</td>
                <td colspan="2" style="border: 1px solid #CCCCCC">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                        RepeatDirection="Horizontal" Width="300px">
                        <asp:ListItem>From Issue</asp:ListItem>
                        <asp:ListItem>Party Wise</asp:ListItem>
                        <asp:ListItem>Manual</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="8">
                    <asp:Panel ID="Panel21" runat="server" Visible="False" Width="913px" BorderWidth="1px">
                        <table style="width: 100%">
                            <tr>
                                <td align="left" style="width: 168px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 112px">
                                    <asp:Label ID="Label12" runat="server" Text="Issue No"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtissueno" runat="server" AutoCompleteType="Disabled" Width="300px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoComplete1" runat="server"
                                        CompletionInterval="100"
                                        CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionListItemCssClass="autocomplete_listItem2"
                                        CompletionSetCount="20"
                                        DelimiterCharacters=","
                                        EnableCaching="false"
                                        MinimumPrefixLength="2"
                                        ServiceMethod="GetIssue"
                                        ServicePath="~/ClientSide/modules/mis/naz/FORMS/HRMS/Forms/WBservices.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true"
                                        TargetControlID="txtissueno">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 168px">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td align="left" style="width: 300px">
                                    <asp:Button ID="btnShowAllData" runat="server" CssClass="BTN2" OnClick="btnShowAllData_Click" Text="Show All Data" Visible="False" Width="115px" />
                                    &nbsp;
                                <asp:Button ID="btnissue" runat="server" CssClass="BTN2" OnClick="btnissue_Click" Text="Post" Visible="False" Width="70px" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvissue" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="GridView1_RowDataBound" Width="100%" BackColor="#003366" CellPadding="4" CellSpacing="1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Reference">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDet_ref" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHdr_DATE" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHdr_Pcode" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDet_Lno" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDet_Icode" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDet_Itm_Desc" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDet_Itm_Uom" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issued">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssueQuantity" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Returned">
                                            <ItemTemplate>
                                                <asp:Label ID="lblretQty" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Return Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReturnQuantity" runat="server" Width="40px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Serial Number">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblserialnumber" runat="server" Width="140px" AutoCompleteType="Disabled"
                                                    TextMode="MultiLine"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoComplete3" runat="server"
                                                    TargetControlID="lblserialnumber"
                                                    ServicePath="~/ClientSide/modules/mis/naz/FORMS/HRMS/Forms/WBservices.asmx"
                                                    ServiceMethod="GetserialL3T"
                                                    MinimumPrefixLength="1"
                                                    CompletionInterval="100"
                                                    EnableCaching="false"
                                                    CompletionSetCount="20"
                                                    CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem2"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    DelimiterCharacters=","
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Store">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStore" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="8">
                    <asp:Panel ID="pnlparty" runat="server" Visible="False" Width="913px" BorderWidth="1px">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">Client Code</td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" colspan="8">
                                    <asp:TextBox ID="txtclientcode" runat="server" Width="525px" AutoPostBack="True" AutoCompleteType="Disabled"
                                        OnTextChanged="txtclientcode_TextChanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                        CompletionInterval="100"
                                        CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionListItemCssClass="autocomplete_listItem2"
                                        CompletionSetCount="20"
                                        DelimiterCharacters=","
                                        EnableCaching="false"
                                        MinimumPrefixLength="2"
                                        ServiceMethod="GetPartyCodeL3T"
                                        ServicePath="~/ClientSide/modules/mis/naz/FORMS/HRMS/Forms/WBservices.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true"
                                        TargetControlID="txtclientcode">
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">Client Name</td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" colspan="8">
                                    <asp:TextBox ID="txtclientName" runat="server" Width="525px" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">
                                    <asp:Label ID="Label7" runat="server" Text="Item Code"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" colspan="8">
                                    <asp:TextBox ID="txtitemcode" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" OnTextChanged="txtitemcode_TextChanged"
                                        Style="margin-bottom: 0px" Width="525px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoComplete2" runat="server"
                                        CompletionInterval="100"
                                        CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionListItemCssClass="autocomplete_listItem2"
                                        CompletionSetCount="20"
                                        DelimiterCharacters=","
                                        EnableCaching="false"
                                        MinimumPrefixLength="1"
                                        ServiceMethod="GetItemCodeL3T"
                                        ServicePath="~/ClientSide/modules/mis/naz/FORMS/HRMS/Forms/WBservices.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true"
                                        TargetControlID="txtitemcode">
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">
                                    <asp:Label ID="Label8" runat="server" Text="Item Description"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" style="width: 220px">
                                    <asp:Label ID="lblitemdescription" runat="server" Width="240px"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 30px">
                                    <asp:Label ID="Label9" runat="server" Text="UoM:"></asp:Label>
                                </td>
                                <td style="width: 25px">
                                    <asp:Label ID="UoM" runat="server" Width="25px"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 39px">Issued:</td>
                                <td align="left" style="width: 30px">
                                    <asp:Label ID="Quantity" runat="server" Width="30px"></asp:Label>
                                </td>
                                <td align="left" style="border: 1px solid #CCCCCC; width: 89px">ReturnQuantity</td>
                                <td align="left" style="width: 43px">
                                    <asp:TextBox ID="txtquantity" runat="server" Width="40px"></asp:TextBox>
                                </td>
                                <td align="left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">
                                    <asp:Label ID="Label11" runat="server" Text="Serial No"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" colspan="8">
                                    <asp:TextBox ID="txtserno" runat="server" AutoCompleteType="Disabled" Style="margin-bottom: 0px"
                                        Width="525px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoComplete4" runat="server"
                                        TargetControlID="txtserno"
                                        ServicePath="~/ClientSide/modules/mis/naz/FORMS/HRMS/Forms/WBservices.asmx"
                                        ServiceMethod="GetserialGen"
                                        MinimumPrefixLength="1"
                                        CompletionInterval="100"
                                        EnableCaching="false"
                                        CompletionSetCount="20"
                                        CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem2"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        DelimiterCharacters=","
                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td align="left" colspan="8">
                                    <asp:Button ID="btnAdd" runat="server" CssClass="BTN2" Text="Add" Width="70px" OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11">
                                    <asp:GridView ID="gvparty" runat="server" Width="100%" OnRowDeleting="gvparty_RowDeleting" OnSelectedIndexChanged="gvparty_SelectedIndexChanged" BackColor="#003366" CellPadding="4" CellSpacing="1">
                                        <Columns>
                                            <%--<asp:CommandField ShowDeleteButton="True" />--%>
                                            <asp:CommandField SelectText="Remove" ShowSelectButton="True" HeaderText="Remove"
                                                ButtonType="Image" SelectImageUrl="~/Images/remove.png">
                                                <ItemStyle ForeColor="Red" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td align="left" colspan="8">
                                    <asp:Button ID="btnReurn" runat="server" CssClass="BTN2" OnClick="btnReurn_Click" Text="Post" Visible="False" Width="70px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlManual" runat="server" Width="913px" BorderWidth="1px" Visible="False">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">Client Code</td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" colspan="8">
                                    <asp:TextBox ID="txtclcode" runat="server" Width="525px">08.01.001.32477</asp:TextBox>
                                    <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                        CompletionInterval="100"
                                        CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionListItemCssClass="autocomplete_listItem2"
                                        CompletionSetCount="20"
                                        DelimiterCharacters=","
                                        EnableCaching="false"
                                        MinimumPrefixLength="2"
                                        ServiceMethod="GetPartyCodeL3T"
                                        ServicePath="~/ClientSide/modules/mis/naz/FORMS/HRMS/Forms/WBservices.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true"
                                        TargetControlID="txtclcode">
                                    </cc1:AutoCompleteExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">Client Name</td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" colspan="8">
                                    <asp:TextBox ID="txtclname" runat="server" Width="525px" ReadOnly="True">Store Return (Subscriber)</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">
                                    <asp:Label ID="Label3" runat="server" Text="Item Code"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" colspan="8">
                                    <asp:TextBox ID="txtitcode" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" OnTextChanged="txtitcode_TextChanged"
                                        Style="margin-bottom: 0px" Width="525px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                        CompletionInterval="100"
                                        CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionListItemCssClass="autocomplete_listItem2"
                                        CompletionSetCount="20"
                                        DelimiterCharacters=","
                                        EnableCaching="false"
                                        MinimumPrefixLength="2"
                                        ServiceMethod="GetItemCodeManualyL3T"
                                        ServicePath="~/ClientSide/modules/mis/naz/FORMS/HRMS/Forms/WBservices.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true"
                                        TargetControlID="txtitcode">
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">
                                    <asp:Label ID="Label5" runat="server" Text="Item Description"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" style="width: 220px">
                                    <asp:Label ID="lblitemdes" runat="server" Width="240px"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 30px">
                                    <asp:Label ID="Label10" runat="server" Text="UoM:"></asp:Label>
                                </td>
                                <td style="width: 25px">
                                    <asp:Label ID="lbluom" runat="server" Width="25px"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 39px">Issued:</td>
                                <td align="left" style="width: 7px">
                                    <asp:Label ID="lbliss" runat="server" Width="30px"></asp:Label>
                                </td>
                                <td align="left" style="border: 1px solid #CCCCCC; width: 89px">ReturnQuantity</td>
                                <td align="left" style="width: 43px">
                                    <asp:TextBox ID="txtqt" runat="server" Width="40px"></asp:TextBox>
                                </td>
                                <td align="left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td style="border: 1px solid #CCCCCC; width: 149px">
                                    <asp:Label ID="Label15" runat="server" Text="Serial No"></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 1px">:</td>
                                <td align="left" colspan="8">
                                    <asp:TextBox ID="txtslno" runat="server" Style="margin-bottom: 0px"
                                        Width="525px"></asp:TextBox>
                                    <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                        TargetControlID="txtslno"
                                        ServicePath="~/ClientSide/modules/mis/naz/FORMS/HRMS/Forms/WBservices.asmx"
                                        ServiceMethod="GetserialGen"
                                        MinimumPrefixLength="1"
                                        CompletionInterval="100"
                                        EnableCaching="false"
                                        CompletionSetCount="20"
                                        CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem2"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        DelimiterCharacters=","
                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                    </cc1:AutoCompleteExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td align="left" colspan="8">
                                    <asp:Button ID="btnAddM" runat="server" CssClass="BTN2" OnClick="btnAddM_Click" Text="Add" Width="70px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11">
                                    <asp:GridView ID="gvManual" runat="server" BorderStyle="None" OnRowDeleting="gvManual_RowDeleting" Width="100%" OnSelectedIndexChanged="gvManual_SelectedIndexChanged" BackColor="#003366" CellPadding="4" CellSpacing="1">
                                        <Columns>
                                            <%--<asp:CommandField ShowDeleteButton="True" />--%>
                                            <asp:CommandField SelectText="Remove" ShowSelectButton="True" HeaderText="Remove"
                                                ButtonType="Image" SelectImageUrl="~/Images/remove.png">
                                                <ItemStyle ForeColor="Red" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 66px">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td align="left" colspan="8">
                                    <asp:Button ID="btnReurnM" runat="server" CssClass="BTN2" OnClick="btnReurnM_Click" Text="Post" Width="70px" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                </td>
            </tr>
        </table>

        <asp:Panel ID="Panel1" runat="server" Style="border-right: black 2px solid; padding-right: 20px; border-top: black 2px solid; display: none; padding-left: 20px; padding-bottom: 20px; border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid; background-color: white"
                    Width="340px">
                    <table id="tblPopUp" runat="server" style="width: 328px">
                        <tr>
                            <td style="height: 21px" align="center" colspan="3">
                                <asp:Label ID="lblMsgHdr" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 151px; height: 21px"></td>
                            <td style="height: 21px"></td>
                            <td style="height: 21px"></td>
                        </tr>
                        <tr>
                            <td style="width: 151px" align="right">Return Ref No</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtIssueRef" ReadOnly="true" Enabled="false" runat="server" BorderStyle="None"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 151px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 18px; text-align: center"></td>
                        </tr>
                    </table>

                    <div style="text-align: center">
                        <asp:Button ID="btnOk" runat="server" Text="Ok" Width="58px" />
                    </div>
                </asp:Panel>

                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="btnOk" PopupControlID="Panel1" TargetControlID="btnOk">
                </cc1:ModalPopupExtender>


    </asp:Panel>
    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server"
        TargetControlID="pnl"
        ExpandControlID="pnlClientInformation"
        CollapseControlID="pnlClientInformation"
        Collapsed="true"
        SuppressPostBack="true"
        ExpandedImage="~/images/collapse.jpg"
        CollapsedImage="~/images/expand.jpg"
        ImageControlID="header_ToggleImage" />

</asp:Content>
