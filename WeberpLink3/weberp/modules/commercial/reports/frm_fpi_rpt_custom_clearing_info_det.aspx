<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_fpi_rpt_custom_clearing_info_det.aspx.cs" Inherits="frm_fpi_rpt_custom_clearing_info_det"  EnableEventValidation="false" %>
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
                CUSTOM CLEARING INFORMATION DETAIL REPORT</td>
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
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
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
                            LC Status</td>
                        <td >
                            &nbsp;</td>
                        <td >
                            :</td>
                        <td style="text-align: left" colspan="2"   >


                                    <asp:RadioButtonList ID="rdolistlcstatus" runat="server" RepeatDirection="Horizontal" Width="208px">
                                        <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="C">Open</asp:ListItem>
                                        <asp:ListItem Value="Y">Close</asp:ListItem>
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
                            LC Date</td>
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
                            &nbsp</td>
                        <td>
                            &nbsp;</td>
                    </tr>



                    <tr>
                        <td style="width: 10%; height: 30px;">
                            </td>
                        <td style="width: 11%; text-align: right; height: 30px;">
                            LC Ref</td>
                        <td style="height: 30px">


                                                                        <asp:CheckBox ID="chkalllc" runat="server" Checked="True" Text="ALL"  Width="50px"/>


                                </td>
                        <td style="height: 30px">
                            :</td>
                        <td style="text-align: left; width: 170px; height: 30px;">

                            <table width ="100%">


                               
                                <td>


                                    <asp:DropDownList ID="ddllcrefno" runat="server"
                                CssClass="txtbox"
                                Width="400px" OnSelectedIndexChanged="ddlloanref_SelectedIndexChanged">
                            </asp:DropDownList>


                                </td>

                            </table>                                            

                        </td>
                        <td style="height: 30px">
                            </td>
                        <td style="height: 30px">
                            </td>
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

