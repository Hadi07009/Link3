<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_mpr_report.aspx.cs" Inherits="frm_mpr_report"  EnableEventValidation="false" %>
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
                MPR REPORT</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblmessage" runat="server" Font-Size="Smaller" ForeColor="#CC3300" style="font-size: medium; font-weight: 700" Text="Message"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">

                <table id="tblcoa" runat="server">

                    <tr>
                        <td style="width: 120px">


                        </td>

                        <td style="width: 133px; text-align: right;">


                            Store</td>

                        <td style="width: 4px">


                            :</td>

                        <td style="width: 412px; ">


                            <asp:DropDownList ID="ddlstore" runat="server" Width="400px">
                            </asp:DropDownList>


                        </td>

                        <td style="width: 412px">


                            &nbsp;</td>

                    </tr>

                </table>


                <table style="width:100%;">
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
                                 Width="85px"  AutoPostBack="True" OnDateChanged="cldtodate_DateChanged">
                                <ButtonStyle CssClass="btn2" />
                            </ew:CalendarPopup>


                                </td>
                                <td style="width: 22px">


                                    TO</td>

                                <td style="text-align: left">


                            <ew:CalendarPopup ID="cldtodate" runat="server" 
                                Culture="English (United Kingdom)" DisableTextBoxEntry="False" Nullable="false" 
                                 Width="85px" style="text-align: left" AutoPostBack="True" OnDateChanged="cldtodate_DateChanged">
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
                    
                    

                    </table>


                <table id="tblcostcenter" runat="server">

                    <tr>
                        <td style="width: 120px">


                        </td>

                        <td style="width: 156px; text-align: right;">


                            MPR Ref.No. </td>

                        <td style="width: 4px">


                            :</td>

                        <td style="text-align: left; width: 52px">


                            <asp:CheckBox ID="chkdeptall" runat="server" Checked="True" Text="ALL" />


                        </td>

                        <td style="width: 412px; text-align: left">


                            <asp:TextBox ID="txtmprref" runat="server" Width="400px"></asp:TextBox>

                            <ajaxToolkit:AutoCompleteExtender
                                runat="server"                                
                                ID="Autoteformprref" 
                                BehaviorID="Autprrefext"                                
                                TargetControlID="txtmprref"
                                ServicePath="~/modules/commercial/services/autocomplete.asmx"
                                ServiceMethod="GetAllMprref"
                                MinimumPrefixLength="3" 
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

