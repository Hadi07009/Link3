<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_app_rule.aspx.cs" Inherits="frm_app_rule" Title=""   EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="update" runat="server">
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
                &nbsp;APPROVAL RULE DETAIL&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 21px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 21px; text-align: center">
                HEADER</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 21px; text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 49px">
                            &nbsp;</td>
                        <td style="width: 146px">
                            APPROVAL</td>
                        <td style="width: 15px">
                            :</td>
                        <td>
                            <asp:DropDownList ID="ddlapptype" runat="server" AutoPostBack="True" 
                                CssClass="txtbox" onselectedindexchanged="ddlapptype_SelectedIndexChanged" 
                                Width="316px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>MPR</asp:ListItem>
                                <asp:ListItem>CS</asp:ListItem>
                                <asp:ListItem>REVISE</asp:ListItem>                               
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 49px">
                            &nbsp;</td>
                        <td style="width: 146px">
                            &nbsp;</td>
                        <td style="width: 15px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 49px">
                            &nbsp;</td>
                        <td style="width: 146px">
                            TYPE OF REQ</td>
                        <td style="width: 15px">
                            :</td>
                        <td>
                            <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True" 
                                CssClass="txtbox" onselectedindexchanged="ddltype_SelectedIndexChanged" 
                                Width="316px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 49px">
                            &nbsp;</td>
                        <td style="width: 146px">
                            &nbsp;</td>
                        <td style="width: 15px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 49px">
                            &nbsp;</td>
                        <td style="width: 146px">
                            TYPE OF PURCHASE</td>
                        <td style="width: 15px">
                            :</td>
                        <td>
                            <asp:DropDownList ID="ddlpurtype" runat="server" AutoPostBack="True" 
                                CssClass="txtbox" onselectedindexchanged="ddlpurtype_SelectedIndexChanged" 
                                Width="316px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>LPO</asp:ListItem>
                                <asp:ListItem>SPO</asp:ListItem>
                                <asp:ListItem>FPO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left; height: 11px;">
                </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 23px; text-align: left">
                
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 23px; text-align: left">
                <table ID="tblheader" runat="server" 
                    style="width: 97%; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa; ">
                    <tr>
                        <td style="width: 34px">
                            SELECT</td>
                        <td style="text-align: left">
                            AMOUNT FROM</td>
                        <td style="text-align: left">
                            AMOUNT TO</td>
                        <td style="text-align: left">
                            APPROVAL FLOW</td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            <asp:CheckBox ID="chk7" runat="server" Text="1." TextAlign="Left" />
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtfrom1" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtfrom1">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtto1" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtto1">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlflow1" runat="server" CssClass="txtbox" Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            <asp:CheckBox ID="chk8" runat="server" Text="2." TextAlign="Left" />
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtfrom2" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtfrom2">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtto2" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtto2">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlflow2" runat="server" CssClass="txtbox" Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            <asp:CheckBox ID="chk9" runat="server" Text="3." TextAlign="Left" />
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtfrom3" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtfrom3">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtto3" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtto3">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlflow3" runat="server" CssClass="txtbox" Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            <asp:CheckBox ID="chk10" runat="server" Text="4." TextAlign="Left" />
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtfrom4" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtfrom4">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtto4" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtto4">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlflow4" runat="server" CssClass="txtbox" Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            <asp:CheckBox ID="chk11" runat="server" Text="5." TextAlign="Left" />
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtfrom5" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtfrom5">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtto5" runat="server" CssClass="txtbox"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" 
                                runat="server" filtertype="Numbers" targetcontrolid="txtto5">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlflow5" runat="server" CssClass="txtbox" Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px; height: 27px">
                        </td>
                        <td style="text-align: left; height: 27px">
                        </td>
                        <td style="text-align: left; height: 27px">
                        </td>
                        <td style="text-align: left; height: 27px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="btnsaveheader" runat="server" CssClass="btn2" 
                                onclick="btnsaveheader_Click" Text="SAVE" Width="117px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 23px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="height: 23px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 23px; text-align: center">
                DETAIL</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 23px; text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 23px; text-align: left">
                APPROVAL FLOW:&nbsp;
                <asp:DropDownList ID="ddlappflow" runat="server" Width="392px" 
                    AutoPostBack="True" CssClass="txtbox" 
                    onselectedindexchanged="ddlappflow_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 23px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: center">
                
                
                <table id="tbldet" runat="server" 
                    style="width: 97%; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='white', startcolorstr='#D6DCF9', gradienttype='0'); border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa; ">
                    <tr>
                        <td style="width: 19px">
                            SL</td>
                        <td style="width: 106px">
                            APPROVAL</td>
                        <td style="width: 116px">
                            ED APP REJ</td>
                        <td style="text-align: center; " colspan="5">
                            FOR TO</td>
                    </tr>
                    <tr>
                        <td style="width: 19px">
                            1.</td>
                        <td style="width: 106px">
                            <asp:DropDownList ID="ddlapp1" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 116px">
                            <asp:CheckBoxList ID="chk1" runat="server" RepeatDirection="Horizontal" 
                                TextAlign="Left">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left; width: 109px;">
                            <asp:DropDownList ID="ddlfor11" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 103px;">
                            <asp:DropDownList ID="ddlfor21" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 102px;">
                            <asp:DropDownList ID="ddlfor31" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 104px;">
                            <asp:DropDownList ID="ddlfor41" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlfor51" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19px">
                            2.</td>
                        <td style="width: 106px">
                            <asp:DropDownList ID="ddlapp2" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 116px">
                            <asp:CheckBoxList ID="chk2" runat="server" RepeatDirection="Horizontal" 
                                TextAlign="Left">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left; width: 109px;">
                            <asp:DropDownList ID="ddlfor12" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 103px;">
                            <asp:DropDownList ID="ddlfor22" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 102px;">
                            <asp:DropDownList ID="ddlfor32" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 104px;">
                            <asp:DropDownList ID="ddlfor42" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlfor52" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19px">
                            3.</td>
                        <td style="width: 106px">
                            <asp:DropDownList ID="ddlapp3" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 116px">
                            <asp:CheckBoxList ID="chk3" runat="server" RepeatDirection="Horizontal" 
                                TextAlign="Left">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left; width: 109px;">
                            <asp:DropDownList ID="ddlfor13" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 103px;">
                            <asp:DropDownList ID="ddlfor23" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 102px;">
                            <asp:DropDownList ID="ddlfor33" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 104px;">
                            <asp:DropDownList ID="ddlfor43" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlfor53" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19px">
                            4.</td>
                        <td style="width: 106px">
                            <asp:DropDownList ID="ddlapp4" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 116px">
                            <asp:CheckBoxList ID="chk4" runat="server" RepeatDirection="Horizontal" 
                                TextAlign="Left">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left; width: 109px;">
                            <asp:DropDownList ID="ddlfor14" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 103px;">
                            <asp:DropDownList ID="ddlfor24" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 102px;">
                            <asp:DropDownList ID="ddlfor34" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 104px;">
                            <asp:DropDownList ID="ddlfor44" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlfor54" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19px">
                            5.</td>
                        <td style="width: 106px">
                            <asp:DropDownList ID="ddlapp5" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 116px">
                            <asp:CheckBoxList ID="chk5" runat="server" RepeatDirection="Horizontal" 
                                TextAlign="Left">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left; width: 109px;">
                            <asp:DropDownList ID="ddlfor15" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 103px;">
                            <asp:DropDownList ID="ddlfor25" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 102px;">
                            <asp:DropDownList ID="ddlfor35" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 104px;">
                            <asp:DropDownList ID="ddlfor45" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlfor55" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19px; height: 8px;">
                            6.</td>
                        <td style="width: 106px; height: 8px;">
                            <asp:DropDownList ID="ddlapp6" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 116px; height: 8px;">
                            <asp:CheckBoxList ID="chk6" runat="server" RepeatDirection="Horizontal" 
                                TextAlign="Left">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 109px;">
                            <asp:DropDownList ID="ddlfor16" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 103px;">
                            <asp:DropDownList ID="ddlfor26" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 102px;">
                            <asp:DropDownList ID="ddlfor36" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 104px;">
                            <asp:DropDownList ID="ddlfor46" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px;">
                            <asp:DropDownList ID="ddlfor56" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19px; height: 8px;">7.</td>
                        <td style="width: 106px; height: 8px;">
                            <asp:DropDownList ID="ddlapp7" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 116px; height: 8px;">
                            <asp:CheckBoxList ID="chk77" runat="server" RepeatDirection="Horizontal" TextAlign="Left">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 109px;">
                            <asp:DropDownList ID="ddlfor17" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 103px;">
                            <asp:DropDownList ID="ddlfor27" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 102px;">
                            <asp:DropDownList ID="ddlfor37" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 104px;">
                            <asp:DropDownList ID="ddlfor47" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px;">
                            <asp:DropDownList ID="ddlfor57" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19px; height: 8px;">8.</td>
                        <td style="width: 106px; height: 8px;">
                            <asp:DropDownList ID="ddlapp8" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 116px; height: 8px;">
                            <asp:CheckBoxList ID="chk88" runat="server" RepeatDirection="Horizontal" TextAlign="Left">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 109px;">
                            <asp:DropDownList ID="ddlfor18" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 103px;">
                            <asp:DropDownList ID="ddlfor28" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 102px;">
                            <asp:DropDownList ID="ddlfor38" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px; width: 104px;">
                            <asp:DropDownList ID="ddlfor48" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 8px;">
                            <asp:DropDownList ID="ddlfor58" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19px; height: 27px">
                            &nbsp;</td>
                        <td style="width: 106px; height: 27px">
                            &nbsp;</td>
                        <td style="width: 116px; height: 27px">
                            &nbsp;</td>
                        <td style="text-align: left; height: 27px; width: 109px;">
                        </td>
                        <td style="text-align: left; height: 27px; width: 103px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 27px; width: 102px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 27px; width: 104px;">
                        </td>
                        <td style="text-align: left; height: 27px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; width: 19px;">
                            &nbsp;</td>
                        <td style="text-align: center; " colspan="7">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; width: 19px;">
                            &nbsp;</td>
                        <td colspan="7" style="text-align: center; ">
                            <asp:Button ID="btnsavedet" runat="server" CssClass="btn2" onclick="btnsavedet_Click" 
                                Text="SAVE" Width="117px" />
                        </td>
                    </tr>
                </table>
                
                
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 52px; text-align: center">
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
</asp:Content>

