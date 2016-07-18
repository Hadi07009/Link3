<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmUserInformation.aspx.cs" Inherits="frmUserInformation"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <cc1:MessageBox ID="MessageBox1" runat="server" />
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />    


    <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
               USER INFORMATON SETUP
            </asp:Panel>
    <div style=" height:1000px; vertical-align:top; ">
    <table class="tblmas" style="width: 100%; vertical-align:text-top; ">
        
    <tr>
        <td >
        <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
        
            <table style="width:100%;">
                <tr>
                    <td style="width: 73px">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 73px">
                        &nbsp;</td>
                    <td colspan="2">
                        <table >
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    &nbsp;</td>
                                <td style="width: 34px">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnImportUser" runat="server" Text="Import User From Employee Database" OnClick="btnImportUser_Click1" Width="344px" />
                                </td>
                                <td rowspan="13" style="text-align: left" valign="top">
                                    <asp:Label ID="lblImage" runat="server" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="Red" Height="155px" Style="text-align: center; vertical-align: middle" Width="155px"> <br /> Photo
                                             <br />  Not <br />  Available  
                                             
                                        </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">SELECT USER</td>
                                <td style="width: 34px">:</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtUserlist" runat="server" AutoCompleteType="None" AutoPostBack="True" ontextchanged="txtUserlist_TextChanged" Width="344px"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender ID="autoComplete1" runat="server" 
                                        BehaviorID="AutoCompleteEx2" 
                                        CompletionInterval="1000" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionSetCount="20" DelimiterCharacters="," 
                                        EnableCaching="false" MinimumPrefixLength="1" 
                                        ServiceMethod="GetUserSearchByUseridEmpidCompanyid" ServicePath="~/services/srvSystem.asmx" 
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtUserlist">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">USER ID/ AD USER ID</td>
                                <td style="width: 34px">:</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtuserid" runat="server" Width="344px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    EMPLOYEE ID</td>
                                <td style="width: 34px">
                                    :</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtEmpcode" runat="server" Width="344px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    NAME</td>
                                <td style="width: 34px">
                                    :</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtName" runat="server"  Width="345px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    SHORT NAME</td>
                                <td style="width: 34px">
                                    :</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtshortname" runat="server" Width="344px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    DESIGNATION</td>
                                <td style="width: 34px">
                                    :</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDesignation" runat="server"  Width="345px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    DEPARTMENT</td>
                                <td style="width: 34px">
                                    :</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDepartment" runat="server"  Width="345px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    EMAIL ID</td>
                                <td style="width: 34px">
                                    :</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtEmail" runat="server"  Width="345px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    MOBILE NO</td>
                                <td style="width: 34px">
                                    :</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtMobile" runat="server"  Width="345px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    &nbsp;</td>
                                <td style="width: 34px">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">
                                    LOGIN STATUS</td>
                                <td style="width: 34px">
                                    :</td>
                                <td style="text-align: left">
                                    <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatDirection="Horizontal" Width="135px">
                                        <asp:ListItem>NO</asp:ListItem>
                                        <asp:ListItem Selected="True">YES</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: right; height: 26px;">
                                </td>
                                <td style="width: 34px; height: 26px;">
                                </td>
                                <td style="height: 26px">
                                </td>
                                <td style="height: 26px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: right">
                                </td>
                                <td style="width: 34px">
                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnAdd" runat="server"  OnClick="btnAdd_Click" 
                                        Text="ADD/UPDATE" Width="104px" />
                                    <asp:Button ID="btnClear" runat="server"  
                                        OnClick="btnClear_Click" Text="CLEAR" Width="104px" />
                                </td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: right; height: 16px;"></td>
                                <td style="width: 34px; height: 16px;"></td>
                                <td style="text-align: left; height: 16px;"></td>
                                <td style="text-align: left; height: 16px;"></td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: right">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: left">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtNewpass" runat="server" Width="345px" TextMode="Password" AutoCompleteType="Disabled" Visible="False"></asp:TextBox>
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 140px; text-align: left">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtConpass" runat="server" Width="345px" TextMode="Password" Visible="False"></asp:TextBox>
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: right">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnPass" runat="server" OnClick="btnPass_Click" Text="Create/Reset Password" Width="161px" Visible="False" />
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: right">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: right">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: right">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:FileUpload ID="upd_image" runat="server" Width="342px" />
                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="btngetimage" runat="server" onclick="btngetimage_Click" Text="Update Image" Width="93px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 136px; text-align: right">&nbsp;</td>
                                <td style="width: 34px">&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:Image ID="imgEmployee" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="200px" Width="200px" />
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 73px">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 73px">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 73px">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 73px">
                        &nbsp;</td>
                    <td style="text-align: left; width: 77px">
                        &nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 73px; height: 40px;">
                    </td>
                    <td style="text-align: left; width: 77px; height: 40px">
                    </td>
                    <td style="text-align: left; height: 40px">
                    </td>
                    <td style="height: 40px">
                        </td>
                </tr>
            </table>
            </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="txtUserlist" EventName="TextChanged" />        
        <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />      
        <asp:PostBackTrigger ControlID="btnAdd"/>     
        <asp:PostBackTrigger ControlID="btngetimage"  />     
        <asp:PostBackTrigger ControlID="btnImportUser" />
        <asp:PostBackTrigger ControlID="btnPass"/>           

        </Triggers>
        </asp:UpdatePanel>
        </td>
    </tr>
</table>

        
        </div>
    


</asp:Content>

