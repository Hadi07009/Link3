<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_item_ledger_report.aspx.cs" Inherits="frm_item_ledger_report"  EnableEventValidation="false" %>
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
                &nbsp;ITEM LEDGER REPORT</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblmessage" runat="server" Font-Size="Smaller" ForeColor="#CC3300" style="font-size: medium; font-weight: 700" Text="Message"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 8%" >
                            &nbsp;</td>
                        <td style="text-align: right; width: 14%;" >
                            REPORT TYPE</td>
                        <td style="width: 1%" >
                            :</td>
                        <td style="text-align: left"   >


                                    <asp:RadioButtonList ID="rdolistreporttype" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">Detail</asp:ListItem>
                                        <asp:ListItem Value="2">Summery</asp:ListItem>
                                    </asp:RadioButtonList>


                        </td>
                        <td align="center" style="width: 3%" >
                            &nbsp;</td>
                        <td style="text-align: left; width: 3%;"  >
                            &nbsp;</td>
                    </tr>
                    
                    

                    <tr>
                        <td style="width: 8%" >
                            &nbsp;</td>
                        <td style="text-align: right; width: 14%;" >
                            STORE</td>
                        <td style="width: 1%" >
                            :</td>
                        <td style="text-align: left"   >


                            <asp:CheckBox ID="chkstore" runat="server" Checked="True" Text="General Store" />


                        </td>
                        <td align="center" style="width: 3%" >
                            &nbsp;</td>
                        <td style="text-align: left; width: 3%;"  >
                            &nbsp;</td>
                    </tr>
                    
                    

                    </table>


                <table id="Table1" runat="server">

                    <tr>
                        <td style="width: 16px">


                        </td>

                        <td style="width: 167px; text-align: right;">


                            DATE </td>

                        <td style="width: 4px">


                            :</td>

                        <td style="text-align: right; width: 76px">


                            From Date</td>

                        <td style="text-align: left; width: 4px">


                            :</td>

                        <td style="text-align: left; width: 131px">


                            <ew:CalendarPopup ID="cldfrdate" runat="server" 
                                Culture="English (United Kingdom)" DisableTextBoxEntry="False" Nullable="false"  
                                 Width="85px" SelectedDate="2014-08-01">
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>


                                </td>

                        <td style="width: 12px; text-align: left">


                            To</td>

                        <td style="width: 4px; text-align: left">


                            :</td>

                        <td style="width: 546px; text-align: left;">


                            <ew:CalendarPopup ID="cldtodate" runat="server" 
                                Culture="English (United Kingdom)" DisableTextBoxEntry="False" Nullable="false"  
                                 Width="85px">
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>


                                </td>

                    </tr>

                </table>





                <table id="tblcoa" runat="server">

                    <tr>
                        <td style="width: 120px">


                            &nbsp;</td>

                        <td style="width: 161px; text-align: right;">


                            ITEM CATEGORY</td>

                        <td>


                            :</td>

                        <td style="text-align: left; width: 50px">


                            <asp:CheckBox ID="chkitemcat" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="text-align: left; width: 65px">


                            &nbsp;</td>

                        <td style="width: 412px; text-align: left">


                            <asp:DropDownList ID="ddlitemcategory" runat="server" Width="400px" OnSelectedIndexChanged="ddlmaingroup_SelectedIndexChanged">
                            </asp:DropDownList>


                        </td>

                        <td style="width: 412px">


                            &nbsp;</td>

                    </tr>

                    <tr>
                        <td style="width: 120px">


                        </td>

                        <td style="width: 161px; text-align: right;">


                            MAIN GROUP</td>

                        <td>


                            :</td>

                        <td style="text-align: left; width: 50px">


                            <asp:CheckBox ID="chkmaingrp" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="text-align: left; width: 65px">


                            &nbsp;</td>

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

                        <td style="text-align: left; width: 56px">


                            &nbsp;</td>

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


                            <asp:CheckBox ID="chksubsub" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="text-align: left; width: 56px">


                            &nbsp;</td>

                        <td style="width: 412px; text-align: left">


                            <asp:DropDownList ID="ddlsubsubgroup" runat="server" Width="400px" OnSelectedIndexChanged="ddlsubsubgroup_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>




                        </td>

                        <td style="width: 412px">


                            &nbsp;</td>

                    </tr>

                    <tr>
                        <td style="width: 117px">


                            &nbsp;</td>

                        <td style="width: 168px; text-align: right;">


                            ITEM</td>

                        <td style="width: 4px">


                            :</td>

                        <td style="text-align: left; width: 55px">


                            <asp:CheckBox ID="chkitm" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="text-align: right; width: 56px">


                            From :</td>

                        <td style="width: 412px; text-align: left">


                            <asp:DropDownList ID="ddlitmfrom" runat="server" Width="400px" >
                            </asp:DropDownList>




                        </td>

                        <td style="width: 412px">


                            &nbsp;</td>

                    </tr>

                    <tr>
                        <td style="width: 117px">


                            &nbsp;</td>

                        <td style="width: 168px; text-align: right;">


                            &nbsp;</td>

                        <td style="width: 4px">


                            &nbsp;</td>

                        <td style="text-align: left; width: 55px">


                            &nbsp;</td>

                        <td style="text-align: right; width: 56px">


                            To :</td>

                        <td style="width: 412px; text-align: left">


                            <asp:DropDownList ID="ddlitmto" runat="server" Width="400px" >
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

