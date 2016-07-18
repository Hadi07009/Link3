<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_current_stock_report.aspx.cs" Inherits="frm_current_stock_report"  EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px; text-align: right;">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="heading" colspan="3" style="text-align: center">
                CURRENT ITEMS STOCK REPORT</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblmessage" runat="server" Font-Size="X-Small" ForeColor="#0099CC" style="font-weight: 700" Text="Message"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 9%" >
                            &nbsp;</td>
                        <td style="text-align: right; width: 15%;" >
                            CURRENT STOCK</td>
                        <td style="width: 3px" >
                            :</td>
                        <td style="text-align: left"   >


                                    <asp:RadioButtonList ID="rdolistitemqty" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">All</asp:ListItem>
                                        <asp:ListItem Value="1">&gt;Zero</asp:ListItem>
                                        <asp:ListItem Value="0">=Zero</asp:ListItem>
                                    </asp:RadioButtonList>


                        </td>
                        <td align="center" >
                            &nbsp;</td>
                        <td style="text-align: left"  >
                            &nbsp;</td>
                    </tr>
                    
                    

                    </table>

                <table id="tblcoa" runat="server">

                    <tr>
                        <td style="width: 120px">


                        </td>

                        <td style="width: 169px; text-align: right;">


                            MAIN GROUP</td>

                        <td style="width: 5px">


                            :</td>

                        <td style="text-align: left; width: 50px">


                            <asp:CheckBox ID="chkmaingrp" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="width: 412px; text-align: left">


                            <asp:DropDownList ID="ddlmaingroup" runat="server" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlmaingroup_SelectedIndexChanged">
                            </asp:DropDownList>


                        </td>

                        <td style="width: 412px">


                            &nbsp;</td>

                    </tr>

                </table>


                <table id="tblcostcenter" runat="server">

                    <tr>
                        <td style="width: 120px">


                        </td>

                        <td style="width: 167px; text-align: right;">


                            SUB GROUP </td>

                        <td style="width: 4px">


                            :</td>

                        <td style="text-align: left; width: 56px">


                            <asp:CheckBox ID="chksubgrp" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="width: 412px; text-align: left">


                            <asp:DropDownList ID="ddlsubgroup" runat="server" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlsubgroup_SelectedIndexChanged">
                            </asp:DropDownList>


                        </td>

                        <td style="width: 412px">


                            &nbsp;</td>

                    </tr>

                </table>


                <table id="tbldepartment" runat="server">

                    <tr>
                        <td style="width: 117px">


                        </td>

                        <td style="width: 168px; text-align: right;">


                            SUB SUB GROUP </td>

                        <td style="width: 4px">


                            :</td>

                        <td style="text-align: left; width: 55px">


                            <asp:CheckBox ID="chksubsubgrp" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="width: 412px; text-align: left">


                            <asp:DropDownList ID="ddlsubsubgroup" runat="server" Width="400px">
                            </asp:DropDownList>




                        </td>

                        <td style="width: 412px">


                            &nbsp;</td>

                    </tr>

                    </table>



                  <table >

                    <tr>
                        <td style="width: 103px">


                        </td>

                        <td style="width: 191px; text-align: right;">


                            &nbsp;</td>

                        <td>


                            &nbsp;</td>

                        <td style="text-align: left; width: 15px">


                            &nbsp;</td>

                        <td style="width: 412px; text-align: left">


                            <asp:Button ID="btnview" runat="server" OnClick="btnview_Click" Text="VIEW" Width="68px" />


                        </td>

                        <td style="width: 412px">


                            &nbsp;</td>

                    </tr>

                    </table>

              
            </td>
        </tr>
        </table>
  

</asp:Content>

