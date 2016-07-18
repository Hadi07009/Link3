<%@ Page Language="C#" CodeFile="login.aspx.cs" Inherits="login_aspx" MasterPageFile="~/masMain.master" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

   

<div align=center>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <scripts>
            <asp:scriptreference Path="~/scripts/membership.js" />
            <asp:scriptreference Path="~/scripts/progressmessage.js" />
        </scripts>
    </asp:ScriptManagerProxy>
    <br />
    <br />
    <br />
    <br />
      
    <table align="center" class="tbl2" cellpadding="3" cellspacing="0" 
        style="border-color:  #FFFFFF; border-style: solid; border-width: thin; width: 47%">
        <tr>
            <td align="right" colspan="2" class="headline"
                >
                LOGIN</td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td align="left">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label2" runat="server" Text="User ID :"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtuserid" runat="server" autocomplete="off" Width="180px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label3" runat="server" Text="Password :"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtpass" runat="server" TextMode="Password" Width="180px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
             &nbsp;<asp:Button ID="btnlogin" runat="server" Text="Login" OnClick="btnlogin_Click" Width="100px" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="lbl" runat="server" ForeColor="Red" Text="Invalid userid or password." Visible="False" Width="193px"></asp:Label>
            </td>
        </tr>
        </table>
        
    <br />
    <span id="waitmsg">&nbsp;</span>
	<br />
    <br />
    <br />
    <br />
        &nbsp;<asp:Panel ID="Panel4" runat="server"  BorderStyle="Solid" BorderWidth="2px" CssClass="tbl2"  DefaultButton="btncancel" Height="205px" HorizontalAlign="Center" ScrollBars="Auto" Style="  display:none; border: 2px solid black; padding: 20px; background-color: white;  " Width="534px">
        <div style="border-color: #e6e6fa; border-width: 1px; text-align:center; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0');
                        width: 100%; height: 205px; text-align: center; ">
            &nbsp;&nbsp;&nbsp;<table style="width: 94%">
                <tr>
                    <td class="tblbig" colspan="3" style="color: #0000CC; font-size: small;">Welcome to Worldcup Football 2014 Prediction League</td>
                </tr>
                <tr>
                    <td colspan="3" style="color: #0000CC; font-size: small;" class="tblbig">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="color: #006600;"><strong>Please Type Your Display Name (3 to 15 Digit)</strong></td>
                </tr>
                <tr>
                    <td class="auto-style9" style="text-align: right;" colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9" colspan="3" style="text-align: center;">
                        <asp:TextBox ID="txtdisplayname" runat="server" autocomplete="off" Width="227px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9" colspan="3" style="text-align: center;">
                        <asp:Label ID="lblexist" runat="server" ForeColor="Red" Text=" Display Name Already Exist" Visible="False" Width="193px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9" style="width: 174px; text-align: right;">&nbsp;</td>
                    <td style="width: 35px; text-align: left">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnok" runat="server" CssClass="btn2" OnClick="btnok_Click" Text="Register" Width="128px" />
                        &nbsp;<asp:Button ID="btncancel" runat="server" Text="Exit" Width="50px" />
                        &nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender5"
                    TargetControlID="Button2">
                </ajaxToolkit:ConfirmButtonExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                     PopupControlID="Panel4" CancelControlID="btncancel" TargetControlID="Button2">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Button ID="Button2" runat="server" Text="Button" Visible="False" />
    </div>        

    <script language="Javascript" >
        function capLock(e) {
            kc = e.keyCode ? e.keyCode : e.which;
            sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);
            if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
                document.getElementById('divMayus').style.visibility = 'visible';
            else
                document.getElementById('divMayus').style.visibility = 'hidden';
        }



        function setfocus(utxtbox, ptxtbox) {

            utxtbox.value = utxtbox.value.toUpperCase();

            //        if (utxtbox.value.length > 2) {            
            //            ptxtbox.focus();
            //        }



        }
        function setfocus2(utxtbox, ptxtbox) {



            eval("utxtbox.focus()");
        }






</script>
</asp:Content>
        
