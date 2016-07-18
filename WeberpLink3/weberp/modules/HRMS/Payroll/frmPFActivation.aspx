<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmPFActivation.aspx.cs" Inherits="modules_HRMS_Payroll_frmPFActivation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <cc1:MessageBox ID="MessageBox1" runat="server" />
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" /> 

    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="PF MEMBERSHIP ACTIVATION" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 100%; text-align: left;">
                    <tr>
                        <td style="width: 122px">&nbsp;</td>
                        <td style="width: 8px">&nbsp;</td>
                        <td align="center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px">&nbsp;</td>
                        <tr>
                            <td style="width: 122px">
                                <asp:Label ID="Label2" runat="server" Text="Select Company"></asp:Label>
                            </td>
                            <td style="width: 8px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" 
                                    OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 122px">&nbsp;</td>
                            <td style="width: 8px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 122px">
                                <asp:Label ID="Label3" runat="server" Text="Employee Code"></asp:Label>
                            </td>
                            <td style="width: 8px">:</td>
                            <td>
                                <asp:TextBox ID="txtEmpId" runat="server" AutoCompleteType="None" AutoPostBack="True" CssClass="btn2" Width="350px"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="txtEmpId_AutoCompleteExtender" runat="server" CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem2" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmpId">
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 122px">&nbsp;</td>
                            <td style="width: 8px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 122px">&nbsp;</td>
                            <td style="width: 8px">&nbsp;</td>
                            <td>
                                <asp:Button ID="btnInclude" runat="server" CssClass="btn2" OnClick="btnInclude_Click" OnClientClick="ShowConfirmBox(this,'Are you active PF ?'); return false;" Text="Active PF" Width="174px" />
                                &nbsp;
                                <asp:Button ID="btnDeactivePF" runat="server" CssClass="btn2" OnClick="btnDeactivePF_Click" OnClientClick="ShowConfirmBox(this,'Are you deactive PF ?'); return false;" Text="Deactive PF" Width="175px" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 122px">&nbsp;</td>
                            <td style="width: 8px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 122px">&nbsp;</td>
                            <td style="width: 8px">&nbsp;</td>
                            <td>
                                <asp:Button ID="btnPFStatus" runat="server" CssClass="btn2"  Text="View PF Status" Width="174px" OnClick="btnPFStatus_Click" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 122px">&nbsp;</td>
                            <td style="width: 8px">&nbsp;</td>
                            <td colspan="2">
                                <table id="tbldet" runat="server" style="width: 100%;">
                                    <tr>
                                        <td style="width: 118px"  >
                                            &nbsp;</td>
                                        <td class="style5" style="width: 5px">&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" >
                                            <asp:GridView ID="GridView1" runat="server">
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 118px; height: 18px;"  >
                                            </td>
                                        <td class="style5" style="width: 5px; height: 18px;"></td>
                                        <td style="height: 18px">
                                            </td>
                                        <td style="height: 18px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 118px">&nbsp;</td>
                                        <td class="style5" style="width: 5px">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 118px"  >
                                            &nbsp;</td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 122px">&nbsp;</td>
                            <td style="width: 8px">&nbsp;</td>
                            <td>
                                <asp:Label ID="lblmsgPf" runat="server" ForeColor="#CC0000"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 122px">&nbsp;</td>
                            <td style="width: 8px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>

            <asp:AsyncPostBackTrigger ControlID="ddlcompany" EventName="SelectedIndexChanged" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnInclude" EventName="Click" />--%>
            <asp:PostBackTrigger ControlID="btnInclude" />   
            <asp:PostBackTrigger ControlID="btnDeactivePF" />   
        </Triggers>
    </asp:UpdatePanel>
    
    <script type="text/javascript">

         function onCalendar1Shown(sender, args) {
             //set the default mode to month            
             sender._switchMode("months", true);
         }
    </script>
</asp:Content>
