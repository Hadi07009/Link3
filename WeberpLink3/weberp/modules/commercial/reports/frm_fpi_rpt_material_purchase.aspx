<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_fpi_rpt_material_purchase.aspx.cs" Inherits="frm_fpi_rpt_material_purchase"  EnableEventValidation="false" %>
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
                RAW MATERIAL PURCHASE REPORT</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: right">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 10%" >
                            &nbsp;</td>
                        <td style="text-align: right; width: 14%;" >
                            Report Type</td>
                        <td >
                            &nbsp;</td>
                        <td >
                            :</td>
                        <td style="text-align: left"   >


                                    <asp:RadioButtonList ID="rdolisttype" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">Detail</asp:ListItem>
                                        <asp:ListItem>Summary</asp:ListItem>
                                    </asp:RadioButtonList>


                        </td>
                        <td align="center" >
                            &nbsp;</td>
                        <td style="text-align: left"  >
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 10%" >
                            &nbsp;</td>
                        <td style="text-align: right; width: 14%;" >
                            PI Type</td>
                        <td >
                            &nbsp;</td>
                        <td >
                            :</td>
                        <td style="text-align: left" colspan="2"   >


                                    <asp:RadioButtonList ID="rdolistpitype" runat="server" RepeatDirection="Horizontal" Width="248px" Height="16px">
                                        <asp:ListItem Selected="True" Value="APP">Approved</asp:ListItem>
                                        <asp:ListItem Value="INI">Innitiated</asp:ListItem>
                                        <asp:ListItem Value="REJ">Rejected</asp:ListItem>
                                    </asp:RadioButtonList>


                        </td>
                        <td style="text-align: left"  >
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 10%" >
                            </td>
                        <td style="text-align: right; width: 14%;" >
                            PI Date</td>
                        <td >
                            &nbsp;</td>
                        <td >
                            :</td>
                        <td   >

                            <table style="width: 135%">
                                <td style="text-align: left; width: 129px">


                            <ew:CalendarPopup ID="cldfrdate" runat="server" 
                                Culture="English (United Kingdom)" DisableTextBoxEntry="False" Nullable="false"  
                                 Width="85px">
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>


                                </td>
                                <td style="width: 22px">


                                    TO</td>

                                <td style="text-align: left">


                            <ew:CalendarPopup ID="cldtodate" runat="server" 
                                Culture="English (United Kingdom)" DisableTextBoxEntry="False" Nullable="false" 
                                 Width="85px" style="text-align: left">
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>


                                </td>

                            </table>

                        </td>
                        <td align="center" >
                            &nbsp;</td>
                        <td style="text-align: left"  >
                            &nbsp;</td>
                        <td >
                          </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;</td>
                        <td style="width: 11%; text-align: right;">
                            Party Name</td>
                        <td>


                                                                        <asp:CheckBox ID="chkallparty" runat="server" Checked="True" Text="ALL"  Width="50px"/>


                                </td>
                        <td>
                            :</td>
                        <td style="text-align: left; width: 170px">


                                    <asp:DropDownList ID="ddlparty" runat="server"
                                CssClass="txtbox"
                                Width="400px">
                            </asp:DropDownList>


                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>



                    <tr>
                        <td style="width: 10%">
                            &nbsp;</td>
                        <td style="width: 11%; text-align: right;">
                            Raw Material</td>
                        <td>


                                                                        <asp:CheckBox ID="chkallitm" runat="server" Checked="True" Text="ALL"  Width="50px"/>


                                </td>
                        <td>
                            :</td>
                        <td style="text-align: left; width: 170px">

                            <table width ="100%">

                                <td>


                                    <asp:DropDownList ID="ddlitem" runat="server"
                                CssClass="txtbox"
                                Width="400px">
                            </asp:DropDownList>


                                </td>

                            </table>                                            

                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>



                    <tr>
                        <td style="width: 10%">
                            &nbsp;</td>
                        <td style="width: 11%; text-align: right;">
                            Group By</td>
                        <td>


                                                                        &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: left; width: 170px">


                                    <asp:RadioButtonList ID="rdolistgroup" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">Supplier wise</asp:ListItem>
                                        <asp:ListItem Value="2">Item wise</asp:ListItem>
                                    </asp:RadioButtonList>


                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>



                    <tr>
                        <td style="width: 10%">
                            &nbsp;</td>
                        <td style="text-align: left; width: 14%">
                            &nbsp;</td>
                        <td style="width: 1%">
                            &nbsp;</td>
                        <td style="width: 1%">
                            &nbsp;</td>
                        <td colspan="3" style="text-align: left">
                            <asp:Button ID="btnview" runat="server" CssClass="btn2" onclick="btnview_Click" 
                                Text="VIEW"  Width="117px" />
                        </td>
                        <td style="width: 1%">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 34px; text-align: left">
                &nbsp;</td>
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
  

</asp:Content>

