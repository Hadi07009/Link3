<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_pf_settle_setup.aspx.cs"  Inherits="frm_pf_settle_setup" EnableEventValidation="false" ValidateRequest="false"  %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
  
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>

       
    <table style="text-align: center; width: 100%; border-left-width: 1px; border-bottom-width: 1px; border-right-width: 1px; vertical-align: middle" >
       
        <tr>
            <td class="cpHeaderContent" >
                PF&nbsp; SETTLEMENT SETUP</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblmessage" runat="server" style="font-size: small"></asp:Label>
            </td>
        </tr>




        
     



        


        <tr>
              <td style="text-align:left;" >

                  &nbsp;</td>
        </tr>




        
     



        


        <tr>
            <td style="text-align:left;" >






              
             <table  id ="tblpf"  runat="server" style="width:100%; ">
   
                 <tr>
                     <td style="width: 144px; height: 10px;"></td>
                     <td style="height: 10px; width: 1453px; font-size: small; text-align: right;">From month</td>
                     <td>:</td>
                     <td style="width: 154px; height: 10px;">
                         <asp:TextBox ID="txtfrommonth" runat="server" Width="220px" ></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="txtstartingodometer_FilteredTextBoxExtender" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Custom, Numbers" TargetControlID="txtfrommonth">
                         </ajaxToolkit:FilteredTextBoxExtender>
                     </td>
                     <td style="width: 1207px; height: 10px; font-size: small; text-align: right;">To month</td>
                     <td style="width: 4px; height: 10px;">:</td>
                     <td style="width: 766px; height: 10px;">
                         <asp:TextBox ID="txttomonth" runat="server" Width="220px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="Filteredtextboxextender1" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Custom, Numbers" TargetControlID="txttomonth">
                         </ajaxToolkit:FilteredTextBoxExtender>
                     </td>
                     <td style="width: 566px; height: 10px;">&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="width: 144px; height: 12px;"></td>
                     <td style="width: 1453px; height: 12px; font-size: small; text-align: right;">Own contribution ratio</td>
                     <td>:</td>
                     <td style="width: 154px; height: 12px;">
                         <asp:TextBox ID="txtowncontribtion" runat="server" Width="220px"></asp:TextBox>

                           <ajaxToolkit:filteredtextboxextender ID="Filteredtextboxextender4" 
            runat="server" Enabled="True" TargetControlID="txtowncontribtion" 
            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="." >
                </ajaxToolkit:filteredtextboxextender>
                     </td>
                     <td style="width: 1207px; height: 12px; font-size: small; text-align: right;">Own profit ratio</td>
                     <td style="width: 4px; height: 12px;">:</td>
                     <td style="width: 766px; height: 12px;">
                         <asp:TextBox ID="txtownprofit" runat="server" Width="220px"></asp:TextBox>

                         <ajaxToolkit:filteredtextboxextender ID="Filteredtextboxextender5" 
            runat="server" Enabled="True" TargetControlID="txtownprofit" 
            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="." >
                </ajaxToolkit:filteredtextboxextender>


                     </td>
                     <td style="width: 566px; height: 12px;">&nbsp;</td>
                 </tr>
    <tr >
        <td  style="width: 144px">
            &nbsp;</td>
        <td  style="width: 1453px; font-size: small; text-align: right;">
            Employer&nbsp; contribution ratio</td>
        <td>
            :</td>
        <td  style="width: 154px">
            <asp:TextBox ID="txtemployeercontribtion" runat="server"  Width="220px"></asp:TextBox>

              <ajaxToolkit:filteredtextboxextender ID="Filteredtextboxextender3" 
            runat="server" Enabled="True" TargetControlID="txtemployeercontribtion" 
            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="." >
                </ajaxToolkit:filteredtextboxextender>
        </td>
        <td style="width: 1207px; font-size: small; text-align: right;">Employer&nbsp; profit ratio</td>
        <td style="width: 4px">:</td>
        <td style="width: 766px">
            <asp:TextBox ID="txtemployeerprofit" runat="server" Width="220px"></asp:TextBox>

            <ajaxToolkit:filteredtextboxextender ID="Filteredtextboxextender2" 
            runat="server" Enabled="True" TargetControlID="txtemployeerprofit" 
            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="." >
                </ajaxToolkit:filteredtextboxextender>
        </td>
        <td style="width: 566px">&nbsp;</td>
    </tr>
                 <tr>
                     <td style="width: 144px">&nbsp;</td>
                     <td style="width: 1453px">&nbsp;</td>
                     <td style="width: 6px">&nbsp;</td>
                     <td style="width: 154px">
                         &nbsp;</td>
                     <td style="width: 1207px; ">&nbsp;</td>
                     <td style="width: 4px">&nbsp;</td>
                     <td style="width: 766px">&nbsp;</td>
                     <td style="width: 566px">&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="width: 144px">&nbsp;</td>
                     <td style="text-align: center;" colspan="6">
                         <asp:Button ID="btadd" runat="server" onclick="btnupdate_Click" Text="Add" Width="85px" />
                         <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" Text="Clear" Width="85px" />
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 144px">&nbsp;</td>
                     <td style="width: 1453px">&nbsp;</td>
                     <td style="width: 6px">&nbsp;</td>
                     <td colspan="2" style="text-align: right;">&nbsp;</td>
                     <td style="width: 4px">&nbsp;</td>
                     <td style="width: 766px">&nbsp;</td>
                     <td style="width: 566px">&nbsp;</td>
                 </tr>
    </table>

                <table  id ="Tbldet"  runat="server" style="width:100%; ">
   
                 <tr>
                     <td colspan="7">
                         <asp:GridView ID="gdvpfsat" runat="server" CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gdvpfsat_SelectedIndexChanged" Width="100%">
                             <RowStyle BackColor="#EFF3FB" />
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#99FF33" />
                             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <EditRowStyle BackColor="#2461BF" />
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField SelectText="Remove" ShowSelectButton="True">
                                 <ItemStyle ForeColor="Red" />
                                 </asp:CommandField>
                             </Columns>
                         </asp:GridView>
                     </td>
                 </tr>
    <tr >
        <td  style="width: 466px; height: 26px;">
            </td>
        <td style="width: 319px; height: 26px;">
            </td>
        <td  style="width: 6px; height: 26px;">
            </td>
        <td  style="text-align: right; height: 26px;">
        </td>
        <td style="width: 4px; height: 26px;"></td>
        <td style="width: 389px; height: 26px;"></td>
        <td style="width: 566px; height: 26px;">
        </td>
    </tr>
    </table>




              </td>
        </tr> 
        
        <tr>
            
            <td style="height: 28px">
                 &nbsp;</td>
        </tr>
        

    </table>
   
             </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                      <asp:AsyncPostBackTrigger ControlID="gdvpfsat" EventName="SelectedIndexChanged" />
                  </Triggers>
    </asp:UpdatePanel>
</asp:Content>

