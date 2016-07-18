<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mat_insp_final.aspx.cs" Inherits="frm_mat_insp_final" Title=""   EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updgrid" runat="server">
 <ContentTemplate>
<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
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
                MATERIAL RECEIVE FOR INSPECTION</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: right">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblparty" runat="server" style="font-weight: 700" Text="PARTY:" 
                    Width="67px"></asp:Label>
                <asp:Label ID="lblpartydet" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <table ID="tblspo" runat="server" style="width:100%;">
                    <tr>
                        <td style="width: 69px">
                            &nbsp;</td>
                        <td style="text-align: left; width: 139px">
                            Purchased By</td>
                        <td style="text-align: left; width: 19px">
                            :</td>
                        <td style="text-align: left">
                            <asp:Label ID="lblby" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 69px">
                            &nbsp;</td>
                        <td style="text-align: left; width: 139px">
                            Purchased From</td>
                        <td style="text-align: left; width: 19px">
                            :</td>
                        <td style="text-align: left">
                            <asp:Label ID="lblfrom" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 69px">
                            &nbsp;</td>
                        <td style="width: 139px">
                            &nbsp;</td>
                        <td style="width: 19px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 34px; text-align: left">
                
                
                <asp:GridView ID="gdItem" runat="server" 
                    BackColor="White" BorderColor="#41519A" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    PageSize="100" SkinID="GridView" 
                    style="border-color: #e6e6fa; border-width: 1px; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); text-align: left;" 
                    Width="98%" OnRowDataBound="gdItem_RowDataBound">
                    <FooterStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#41519A" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle BackColor="#41519A" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" 
                        Font-Underline="False" />
                    <RowStyle Font-Size="8pt" />
                  
                </asp:GridView>
                
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
            
             <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BorderWidth="2px" CssClass="tbl"
                    DefaultButton="btncancel" Height="150px" ScrollBars="Auto"  Style="border-right: black 2px solid;
                    padding-right: 20px; border-top: black 2px solid; padding-left: 20px; display:none;
                    padding-bottom: 20px;  border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
                    background-color: white" Width="329px">
                    <div style="border-color: #e6e6fa; border-width: 1px; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0');
                        width: 94%; height: 135px; text-align: center; ">
                        &nbsp;&nbsp;<table id="tblmsg" runat="server" 
                            style="width: 286px; height: 109px;">
                        <tr>
                            <td colspan="1" style="width: 364px; height: 18px; text-align: center">
                                <span style="color: #ff0000"><strong>&nbsp;MATERIAL RECEIVED SUCCESSFULLY</strong></span></td>
                        </tr>
                        <tr>
                            <td style="width: 364px; height: 13px; text-align: center;">
                                LOG REF:<asp:Label ID="lbllogref" runat="server" Font-Bold="True" Width="211px"></asp:Label>
                            </td>
                        </tr>
                            <tr>
                                <td colspan="1" style="height: 19px; text-align: left; width: 364px;">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="1" style="width: 364px; height: 29px; text-align: center">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn2" Width="0px" Height="0px" />
                                     <asp:Button ID="btncancelhdn" runat="server" Width="0px" Height="0px" 
                                        CssClass="hdn" />
                                    <asp:Button ID="btnok" runat="server" CssClass="btn2" OnClick="btnok_Click" Text="OK"
                                        Width="102px" />
                                        <asp:Button ID="btnprint" runat="server" CssClass="btn2" 
                                        onclick="btnprint_Click" Text="Print" Width="102px" />
                                        </td>
                            </tr>
                    </table>
                    </div>
                </asp:Panel>
                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender5"
                    TargetControlID="Button1">
                </ajaxToolkit:ConfirmButtonExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="btncancelhdn" PopupControlID="Panel4" TargetControlID="Button1">
                </ajaxToolkit:ModalPopupExtender> 
                <asp:Button ID="Button2" runat="server" Text="Button" Visible="False" />
            
               
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
                Comments (If any):
                <asp:TextBox ID="txtcomm" runat="server" CssClass="txtbox" Width="454px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
                <asp:Button ID="btnCancel" runat="server" CssClass="btn2" Text="Cancel" 
                    Width="121px" onclick="btnCancel_Click" />
                &nbsp;<asp:Button ID="btnconfirm" runat="server" CssClass="btn2" Text="Confirm" 
                    Width="121px" onclick="btnconfirm_Click" />
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: center">
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
</ContentTemplate>                
</asp:UpdatePanel>
 <script type="text/javascript">


         function ColorRow(CheckBoxObj) {
             if (CheckBoxObj.checked == true) {
                 CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#88AAFF';
             }
             else {
                 CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#F8E5A1';
             }

         }
         
         function ShowHideField(DecisionControl, ToggleControl1) {

             if (DecisionControl.checked == true) {
                 ToggleControl1.style.visibility = 'visible';                 
             }
             else 
             {
                 ToggleControl1.style.visibility = 'hidden';                 
             }

         }

         
        
    </script>
</asp:Content>

