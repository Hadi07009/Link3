<%@ Page Language="C#" MasterPageFile="masMain.master" AutoEventWireup="true" CodeFile="frmLogin.aspx.cs" Inherits="frmLogin"  ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
       
function ok(sender, e)
{

    __doPostBack('lnkLogin', e); 
    
}



</script>

    <table  width="100%" id="tblmaster" runat="server" >
        <tr>
            <td style="height: 18px">
            
            </td>
            <td style="height: 18px">
            </td>
            <td style="height: 18px">
            </td>
        </tr>
        <tr>
            <td style="height: 18px;">
            </td>
            <td style="height: 18px; text-align: center;">
                <asp:LinkButton ID="lnkLogin" runat="server"  Width="105px"></asp:LinkButton></td>
            <td style="height: 18px">
            </td>
        </tr>
        <tr>
            <td style="height: 71px">
            </td>
            <td style="text-align: center; height: 71px;">
                
                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style=" display:none;" 
                    Width="440px" DefaultButton="btnLogin" Height="228px" >
                <asp:Panel ID="Panel2" runat="server" CssClass="heading" Width="440px" style="cursor: pointer">
                    LINKOFFICE
                    LOGIN</asp:Panel>
                    <div>
                       
                            <table style="width: 410px; left: 0px; vertical-align: middle; top: 0px; text-align: center;">
                    <tr>
                        <td  style="width: 55px; height: 36px; text-align: left">
                      
                        </td>
                        <td  style="width: 74px; text-align: left; height: 36px;">
                        </td>
                        <td  style="width: 3px; height: 36px; text-align: left">
                        </td>
                        <td style="text-align: left; width: 234px; height: 36px;">
                        </td>
                    </tr>
                    <tr>
                        <td  style="width: 55px; text-align: left">
                        </td>
                        <td  style="width: 74px; text-align: left">
                            User Name</td>
                        <td  style="width: 3px; text-align: left">
                            :</td>
                        <td style="text-align: left; width: 234px;">
                            <asp:TextBox ID="txtuserid" runat="server"  Width="148px">
                            </asp:TextBox>
                             <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtuserid" 
            WatermarkCssClass="Watermark" WatermarkText="Enter your user id.." />
                                                </td>
                    </tr>
                    <tr>
                        <td style="width: 55px; height: 10px">
                        </td>
                        <td style="width: 74px; height: 10px;">
                        </td>
                        <td style="width: 3px; height: 10px">
                        </td>
                        <td style="width: 234px; height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td  style="width: 55px; text-align: left">
                        </td>
                        <td  style="width: 74px; text-align: left">
                            Password</td>
                        <td  style="width: 3px; text-align: left">
                            :</td>
                        <td style="text-align: left; width: 234px;">
                            <asp:TextBox ID="txtpass" runat="server"  TextMode="Password"    Width="148px"></asp:TextBox>
                           
                            </td>
                    </tr>
                    <tr>
                        <td colspan="1" style="width: 55px; height: 16px; text-align: left">
                        </td>
                        <td colspan="3" style="text-align: left; height: 16px;">
                        <span><div id="divMayus" style="visibility:hidden; color: #FF0000">Caps Lock is on.</div> </span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" style="width: 55px; text-align: center">
                        </td>
                        <td colspan="3" style="text-align: center">
            <asp:Label ID="lbl" runat="server" Text="                      Invalid userid or password." Visible="False"
                                                    Width="193px" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="1" style="width: 55px; height: 15px; text-align: center">
                        </td>
                        <td colspan="3" style="text-align: center; height: 15px;">
                        </td>
                    </tr>
                                <tr>
                                    <td colspan="1" style="width: 55px; height: 25px; text-align: center">
                                    </td>
                                    <td colspan="3" style="height: 25px; text-align: center">
                                        <asp:Button ID="btnLogin" runat="server" Text="LogIn" Width="94px"   /></td>
                                </tr>
                </table>
                        <p style="text-align: center">
                            &nbsp;
                        </p>
                    </div>
                </asp:Panel>
                &nbsp;
            </td>
            <td style="height: 71px">
            </td>
        </tr>
        <tr>
            <td style="height: 25px">
            </td>
            <td style="text-align: center; height: 25px;">
                &nbsp;<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                   DropShadow="true" TargetControlID="lnkLogin" OkControlID="btnLogin" OnOkScript="ok()" 
                   PopupControlID="Panel1" PopupDragHandleControlID="Panel2"  >
                </ajaxToolkit:ModalPopupExtender>
                
                &nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td style="height: 25px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: center">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
            <td style="height: 15px; text-align: center">
            </td>
            <td style="height: 15px">
            </td>
        </tr>
    </table>

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

        utxtbox.value=utxtbox.value.toUpperCase();

//        if (utxtbox.value.length > 2) {            
//            ptxtbox.focus();
//        }


       
    }
    function setfocus2(utxtbox, ptxtbox) {
       


        eval("utxtbox.focus()");
    }
        
  
        
   
   

</script>


</asp:Content>