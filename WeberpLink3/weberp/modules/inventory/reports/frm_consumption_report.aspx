<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_consumption_report.aspx.cs" Inherits="frm_consumption_report"  EnableEventValidation="false" %>
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
                ITEM CONSUMPTION REPORT</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblmessage" runat="server" Font-Size="Smaller" ForeColor="#3366CC" style="font-size: 12px; font-weight: 700" Text="Message"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 9%" >
                            &nbsp;</td>
                        <td style="text-align: right; width: 14%;" >
                            Report Type</td>
                        <td style="width: 3px" >
                            :</td>
                        <td style="text-align: left"   >


                                    <asp:RadioButtonList ID="rdolistreporttype" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
                                        <asp:ListItem>Summary</asp:ListItem>
                                    </asp:RadioButtonList>


                        </td>
                        <td align="center" >
                            &nbsp;</td>
                        <td style="text-align: left"  >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 9%" >
                            </td>
                        <td style="text-align: right; width: 14%;" >
                            &nbsp;Date</td>
                        <td style="width: 3px" >
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
                    </tr>
                    <tr>
                        <td style="width: 9%">
                            &nbsp;</td>
                        <td style="width: 11%; text-align: right;">
                            Group By</td>
                        <td style="width: 9px">
                            :</td>
                        <td style="text-align: left; width: 170px">


                                    <asp:RadioButtonList ID="rdolistgroupby" runat="server" RepeatDirection="Horizontal" Width="333px">
                                        <asp:ListItem Selected="True" Value="0">Item COA wise</asp:ListItem>
                                        <asp:ListItem Value="1">Cost Center wise</asp:ListItem>
                                        <asp:ListItem Value="2">Department wise</asp:ListItem>
                                    </asp:RadioButtonList>


                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>

                    

                    </table>

                <table id="tblcoa" runat="server">

                    <tr>
                        <td style="width: 120px">


                        </td>

                        <td style="width: 176px; text-align: right;">


                            Item COA Code</td>

                        <td style="width: 4px">


                            :</td>

                        <td style="text-align: left; width: 54px">


                            <asp:CheckBox ID="chkcoaall" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="width: 412px; text-align: left">


                            <asp:DropDownList ID="ddlcoacode" runat="server" Width="400px">
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


                            Department </td>

                        <td style="width: 4px">


                            :</td>

                        <td style="text-align: left; width: 52px">


                            <asp:CheckBox ID="chkdeptall" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="width: 412px; text-align: left">


                            <asp:DropDownList ID="ddldepartment" runat="server" Width="400px">
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

                        <td style="width: 180px; text-align: right;">


                            Cost Center&nbsp; </td>

                        <td style="width: 4px">


                            :</td>

                        <td style="text-align: left; width: 55px">


                            <asp:CheckBox ID="chkcostcenter" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="width: 412px; text-align: left">


                            <asp:TextBox ID="txtcostcenter" runat="server" Width="392px"></asp:TextBox>

                             <ajaxToolkit:AutoCompleteExtender
                                runat="server"                                
                                ID="autocomplete22" 
                                BehaviorID="autocom2"                                
                                TargetControlID="txtcostcenter"
                                ServicePath="~/services/srvSystem.asmx"
                                ServiceMethod="GetCOACode"
                                MinimumPrefixLength="1" 
                                CompletionInterval="1000"
                                EnableCaching="false"
                                CompletionSetCount="20"                                 
                                CompletionListCssClass ="autocomplete_completionListElement"                            
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters=","
                                ShowOnlyCurrentWordInCompletionListItem="true" > 
                            </ajaxToolkit:AutoCompleteExtender>




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

