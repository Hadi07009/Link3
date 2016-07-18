<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmNodeEdit.aspx.cs" Inherits="frmNodeEdit"   EnableEventValidation="false"   ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style=" height:1000px; vertical-align:top; ">
       <table style="width: 100%" class="tblmas">
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="text-align: center">
                TREE NODE XML DETAILS&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
                <asp:TextBox ID="txtXml" runat="server"  Height="59px" TextMode="MultiLine"
                    Width="95%" Wrap="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 27px">
            </td>
            <td style="height: 27px; text-align: center">
                &nbsp;</td>
            <td style="height: 27px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: center">
                &nbsp;<asp:Button ID="btnReload" runat="server"  OnClick="btnReload_Click"
                    Text="Reload" Width="140px" /><asp:Button ID="btnUpdate" runat="server" CssClass="btn2"
                        OnClick="btnUpdate_Click" Text="Update" Width="140px" /></td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 73px">
            </td>
            <td style="height: 73px; text-align: center">
                <asp:Panel ID="PNL" runat="server"  Style="border-right: black 2px solid;
                    padding-right: 20px; border-top: black 2px solid; display: none; padding-left: 20px;
                    padding-bottom: 20px; border-left: black 2px solid; width: 200px; padding-top: 20px;
                    border-bottom: black 2px solid; background-color: white">
                    Are you sure you want to update tree XML?
                    <br />
                    <br />
                    <div style="text-align: right">
                        <asp:Button ID="ButtonOk" runat="server"  Text="OK" />
                        <asp:Button ID="ButtonCancel" runat="server"  Text="Cancel" />
                    </div>
                </asp:Panel>
                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="ModalPopupExtender1"
                    TargetControlID="btnUpdate">
                </ajaxToolkit:ConfirmButtonExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="ButtonCancel" OkControlID="ButtonOk" PopupControlID="PNL" TargetControlID="btnUpdate">
                </ajaxToolkit:ModalPopupExtender>
            </td>
            <td style="height: 73px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>

        
        </div>
    


</asp:Content>

