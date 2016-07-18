<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="RevalueationAsset.aspx.cs" Inherits="modules_FixedAsset_TransactionDetails_RevalueationAsset" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register assembly="eWorld.UI.Compatibility" namespace="eWorld.UI.Compatibility" tagprefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
        <asp:Label ID="lblleave" Text="Revaluation of Asset" runat="server" />
    </asp:Panel>
    
    <table style="width:100%; text-align:left">
        
        <tr>
            <td style="width: 128px"  >
                &nbsp;</td>
            <td style="width: 14px"  >
                &nbsp;</td>
            <td style="width: 410px"   >
                &nbsp;</td>
            <td style="text-align:left">
                &nbsp;</td>
        </tr>
        
        <tr>
            <td style="width: 128px"  >
                <asp:Label ID="Label1" runat="server" Text="Item Code"></asp:Label>
            </td>
            <td style="width: 14px"  >
                :</td>
            <td style="width: 410px"   >
                <asp:TextBox ID="txtItemSearch" runat="server"  
                                                 Width="400px" AutoPostBack="True" OnTextChanged="txtItemSearch_TextChanged" ></asp:TextBox>
                 <cc1:AutoCompleteExtender ID="txtItemSearch_AutoCompleteExtender"
                      runat="server" DelimiterCharacters="" 
                     Enabled="True" 
                     ServicePath="~/modules/FixedAsset/services/InvAutoComplete.asmx" 
                     MinimumPrefixLength="1"                      
                     ServiceMethod="GetItemCodeForRevalueation"  
                     TargetControlID="txtItemSearch">
                </cc1:AutoCompleteExtender>
            </td>
            <td style="text-align:left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">
                <asp:Label ID="lblTrackingNo" runat="server" Text="Tracking No"></asp:Label>
            </td>
            <td style="width: 14px">:</td>
            <td style="width: 410px">
                <asp:DropDownList ID="ddlTrackingNo" runat="server" Width="405px" AutoPostBack="True" OnSelectedIndexChanged="ddlTrackingNo_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">
                <asp:Label ID="Label2" runat="server" Text="Item Name"></asp:Label>
            </td>
            <td style="width: 14px">:</td>
            <td style="width: 410px">
                <asp:Label ID="lblItemName" runat="server"></asp:Label>
            </td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">
                <asp:Label ID="Label3" runat="server" Text="Item Initial Value"></asp:Label>
            </td>
            <td style="width: 14px">:</td>
            <td style="width: 410px">
                <asp:Label ID="lblItemInitialValue" runat="server"></asp:Label>
            </td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">
                <asp:Label ID="Label4" runat="server" Text="WD Value"></asp:Label>
            </td>
            <td style="width: 14px">:</td>
            <td style="width: 410px">
                <asp:Label ID="lblUODValue" runat="server"></asp:Label>
            </td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">
                <asp:Label ID="Label5" runat="server" Text="Trn Type"></asp:Label>
            </td>
            <td style="width: 14px">:</td>
            <td style="width: 410px">
                <asp:DropDownList ID="ddlTrnType" runat="server" Width="405px">
                </asp:DropDownList>
            </td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">
                <asp:Label ID="Label6" runat="server" Text="Amount"></asp:Label>
            </td>
            <td style="width: 14px">:</td>
            <td style="width: 410px">
                <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumberKey(event)" Width="400px"></asp:TextBox>
            </td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">
                <asp:Label ID="Label7" runat="server" Text="Trn Date"></asp:Label>
            </td>
            <td style="width: 14px">:</td>
            <td style="width: 410px">
                <ew:CalendarPopup ID="popupTrnDate" runat="server" AutoPostBack="True" Culture="English (United Kingdom)" Enabled="true" Width="210px">
                    <MonthHeaderStyle BackColor="#2A2965" />
                    <ButtonStyle CssClass="btn2" />
                </ew:CalendarPopup>
            </td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">&nbsp;</td>
            <td style="width: 14px">&nbsp;</td>
            <td style="width: 410px">
                <asp:Label ID="lblLineNo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblRefNumber" runat="server" Visible="False"></asp:Label>
            </td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">&nbsp;</td>
            <td style="width: 14px">&nbsp;</td>
            <td style="width: 410px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
            </td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 128px">&nbsp;</td>
            <td style="width: 14px">&nbsp;</td>
            <td style="width: 410px">&nbsp;</td>
            <td style="text-align:left">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="grdItemRevalueation" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdItemRevalueation_RowCommand" OnRowDataBound="grdItemRevalueation_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <%# Container.DisplayIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code ">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCode" Text='<%# Bind("ItemCode") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tracking No ">
                            <ItemTemplate>
                                <asp:Label ID="lblTrackingNumber" runat="server" Text='<%# Bind("TrackingNo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trn Type">
                            <ItemTemplate>
                                <asp:Label ID="lblTrnTypeName" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trn Type Value">
                            <ItemTemplate>
                                <asp:Label ID="lblTrnType" runat="server" Text='<%# Bind("TrnType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("TrnAmount","{0:0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trn Date ">
                            <ItemTemplate>
                                <asp:Label ID="lblTrnDate" runat="server" Text='<%# Bind("TrnDate", "{0:d}") %>' ></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Line Number">
                            <ItemTemplate>
                                <asp:Label ID="lblLineNo" runat="server" Text='<%# Bind("LineNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RefNo">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width: 128px"  >
                &nbsp;</td>
            <td style="width: 14px"  >
                &nbsp;</td>
            <td style="width: 410px"   >
                &nbsp;
                </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>


            </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnSave" />--%>
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

