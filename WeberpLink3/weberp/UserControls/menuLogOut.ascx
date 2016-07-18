<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menuLogOut.ascx.cs" Inherits="UserControls_menuLogOut" %>
<%@ Implements Interface="System.Web.UI.WebControls.WebParts.IWebPart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<ul id="Ul1" class="menutextindent" runat=server  style=" margin-top:10px;">

    <%--<li id="li1" runat=server >
        <asp:LinkButton ID="lnkMyprofile" runat="server" CssClass="menutextindent" OnClick="lnkMyprofile_Click">My Profile</asp:LinkButton>
    </li>--%>
    
    <li id="li2" runat=server >
        <asp:LinkButton ID="lnkChangepass" runat="server" CssClass="menutextindent" OnClick="lnkChangepass_Click">Change Password</asp:LinkButton>
         <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="ModalPopupExtender2"
             TargetControlID="lnkChangepass">
         </ajaxToolkit:ConfirmButtonExtender>
         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
             CancelControlID="ButtonCancel" OkControlID="ButtonOk" PopupControlID="PNL" TargetControlID="lnkLogout">
         </ajaxToolkit:ModalPopupExtender>
        &nbsp;
         <asp:Panel ID="Panel1" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
             border-top: black 2px solid; display: none; padding-left: 20px; padding-bottom: 20px;
             border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
             background-color: white" CssClass="tbl" Visible="False" Width="340px">
             <table style="width: 328px" class="tblmas">
                 <tr>
                     <td style="width: 151px; height: 21px">
                     </td>
                     <td style="height: 21px">
                     </td>
                     <td style="height: 21px">
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 151px">
                         Current Password</td>
                     <td>
                         :</td>
                     <td>
                         <asp:TextBox ID="txtpass" runat="server" CssClass="txtbox" TextMode="Password"></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td style="width: 151px">
                         New Password</td>
                     <td>
                         :</td>
                     <td>
                         <asp:TextBox ID="txtNewpass" runat="server" CssClass="txtbox" TextMode="Password"></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td style="width: 151px">
                         Confirm New Password</td>
                     <td>
                         :</td>
                     <td>
                         <asp:TextBox ID="txtConpass" runat="server" CssClass="txtbox" TextMode="Password"></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td colspan="3" style="height: 18px; text-align: center">
                         </td>
                 </tr>
             </table>
             <div style="text-align: right">
                 <asp:Button ID="btnCOk" runat="server"  Text="Update" CssClass="btn2" Width="58px" />
                 <asp:Button ID="btnCCancel" runat="server"  Text="Cancel" CssClass="btn2"  /></div>
         </asp:Panel>
         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
             CancelControlID="btnCCancel" OkControlID="btnCOk" PopupControlID="Panel1" TargetControlID="lnkChangepass">
         </ajaxToolkit:ModalPopupExtender>
         <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="ModalPopupExtender1"
             TargetControlID="lnkLogout">
         </ajaxToolkit:ConfirmButtonExtender>

        <asp:LinkButton ID="lnkLogout" runat="server" CssClass="menutextindent" OnClick="lnkLogout_Click" Visible="False">Log Out</asp:LinkButton>
         &nbsp;&nbsp;
         <asp:Panel ID="PNL" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
             border-top: black 2px solid;  padding-left: 20px; padding-bottom: 20px; display:none;
             border-left: black 2px solid; width: 240px; padding-top: 20px; border-bottom: black 2px solid;
             background-color: white" CssClass="tbl" Visible="False" Width="223px">
             Are you sure you want to Log Out?
             <br />
             <br />
             <div style="text-align: right">
                 <asp:Button ID="ButtonOk" runat="server"  Text="Yes" CssClass="btn2" Width="58px" />
                 <asp:Button ID="ButtonCancel" runat="server"  Text="No" CssClass="btn2" Width="56px"  />
             </div>
         </asp:Panel>
         <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Visible="False" />
         <asp:Panel ID="Panel2" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
             border-top: black 2px solid; display: none; padding-left: 20px; padding-bottom: 20px;
             border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
             background-color: white" CssClass="tbl" Visible="False" ForeColor="Blue" Width="232px" Font-Size="Medium">
             Updated Successfully <br />
             <br />
             <div style="text-align: right">
                 <asp:Button ID="btnpcok" runat="server"  Text="OK" CssClass="btn2" Width="80px" />
             </div>
         </asp:Panel>
         <asp:Panel ID="Panel3" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
             border-top: black 2px solid; display: none; padding-left: 20px; padding-bottom: 20px;
             border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
             background-color: white" CssClass="tbl" Visible="False" ForeColor="Red" Width="232px" Font-Size="Medium">
             Password Not Matching. Please Try Again.&nbsp;<br />
             <br />
             <div style="text-align: right">
                 &nbsp;<asp:Button ID="btnpccan" runat="server"  Text="OK" CssClass="btn2" Width="77px" />&nbsp;</div>
         </asp:Panel>
         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
             CancelControlID="btnpcok"  PopupControlID="Panel2" TargetControlID="Button1">
         </ajaxToolkit:ModalPopupExtender>
         <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="ModalPopupExtender3"
             TargetControlID="Button1">
         </ajaxToolkit:ConfirmButtonExtender>
         <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" DisplayModalPopupID="ModalPopupExtender4"
             TargetControlID="Button1">
         </ajaxToolkit:ConfirmButtonExtender>
         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
             CancelControlID="btnpccan" PopupControlID="Panel3" TargetControlID="Button1">
         </ajaxToolkit:ModalPopupExtender>
       
         <asp:Panel ID="Panel4" runat="server" CssClass="tbl" Style="border-right: black 2px solid;
        padding-right: 20px; border-top: black 2px solid;  padding-left: 20px; display: none;
        padding-bottom: 20px; border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: white" Visible="False" Width="563px">
        <table id="tbl_usr_profile" runat="server" class="tblmas" style="width: 517px">
            <tr>
                <td style="width: 151px; height: 21px">
                </td>
                <td style="width: 7px; height: 21px">
                </td>
                <td style="height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 151px">
                USER CODE</td>
                <td style="width: 7px">
                    :</td>
                <td>
                    <asp:Label ID="lblid" runat="server" CssClass="tbl"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 151px">
                NAME</td>
                <td style="width: 7px">
                    :</td>
                <td>
                    <asp:TextBox ID="txtname" runat="server" CssClass="txtbox" Width="328px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 151px">
                DESIGNATION</td>
                <td style="width: 7px">
                    :</td>
                <td>
                    <asp:TextBox ID="txtdesig" runat="server" CssClass="txtbox" Width="328px"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 151px">
                DEPARTMENT</td>
                <td style="width: 7px">
                    :</td>
                <td>
                    <asp:TextBox ID="txtdept" runat="server" CssClass="txtbox"  Width="328px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 151px">
                EMAIL ID</td>
                <td style="width: 7px">
                    :</td>
                <td>
                    <asp:TextBox ID="txtemail" runat="server" CssClass="txtbox" Width="328px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 18px; text-align: center">
                </td>
            </tr>
        </table>
        <div style="text-align: center">
            <asp:Button ID="btnprofileok" runat="server" CssClass="btn2" Text="CANCEL" 
                Width="90px" />&nbsp;
            <asp:Button ID="btnprofileupdate" runat="server" CssClass="btn2" 
                onclick="btnprofileupdate_Click" Text="UPDATE" Width="90px" />
            </div>
    </asp:Panel>
         <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender5"
             TargetControlID="Button1">
         </ajaxToolkit:ConfirmButtonExtender>
         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
             CancelControlID="btnprofileok" PopupControlID="Panel4" TargetControlID="Button1">
         </ajaxToolkit:ModalPopupExtender>

    </li>
    
     <%--<li id="li3" runat=server >
        
    </li>--%>
            
</ul>
