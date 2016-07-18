<%@ Page Title="" Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<%@ Register src="UserControls/ucSelfService.ascx" tagname="ucSelfService" tagprefix="uc1" %>


<%@ Register src="UserControls/ucTeaskPending.ascx" tagname="ucTeaskPending" tagprefix="uc2" %>


<%@ Register src="UserControls/ucNoticeBoard.ascx" tagname="ucNoticeBoard" tagprefix="uc3" %>
<%@ Register src="UserControls/ucFormsPolicy.ascx" tagname="ucFormsPolicy" tagprefix="uc4" %>


<%@ Register src="UserControls/ucEmployeeInformation.ascx" tagname="ucEmployeeInformation" tagprefix="uc5" %>


<%@ Register src="UserControls/ucOthers.ascx" tagname="ucOthers" tagprefix="uc6" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      
        
     <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                DASHBOARD
            </asp:Panel>
    <table style="width:100%;">
        <tr>
            <td style="width: 50px">&nbsp;</td>
            <td style="width: 300px">&nbsp;</td>
            <td style="width: 30px">&nbsp;</td>
            <td style="width: 300px">&nbsp;</td>
            <td style="width:30px">&nbsp;</td>
            <td style="width: 300px">&nbsp;</td>
           <td style="width: 50px">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50px">&nbsp;</td>
            <td style="width:300px; text-align:left; vertical-align:top">
                <uc1:ucSelfService ID="ucSelfService1" runat="server" />
            </td>
            <td style="width:30px">
                &nbsp;</td>
            <td style="width:300px; text-align:left; vertical-align:top">
                <uc5:ucEmployeeInformation ID="ucEmployeeInformation1" runat="server" />
            </td>
             <td style="width:30px">&nbsp;</td>
            <td style="width:300px; text-align:left; vertical-align:top">
                <uc2:ucTeaskPending ID="ucTeaskPending1" runat="server" />
            </td>
            <td style="width: 50px">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50px">&nbsp;</td>
            <td style="width: 300px">&nbsp;</td>
            <td style="width: 30px">&nbsp;</td>
            <td style="width:300px; text-align:left; vertical-align:top"></td>
            <td style="width:30px"></td>
            <td style="width: 300px">&nbsp;</td>
            <td style="width: 50px">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50px">&nbsp;</td>
            <td style="width: 300px">
                <uc4:ucFormsPolicy ID="ucFormsPolicy1" runat="server" />
            </td>
            <td style="width: 30px">&nbsp;</td>
             <td style="width:300px; text-align:left; vertical-align:top">
                 <uc6:ucOthers ID="ucOthers1" runat="server" />
            </td>
            <td style="width:30px"></td>
            <td style="width: 300px">
                <uc3:ucNoticeBoard ID="ucNoticeBoard1" runat="server" />
            </td>
           <td style="width: 50px">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50px">&nbsp;</td>
            <td style="width: 300px">
                &nbsp;</td>
            <td style="width: 30px">&nbsp;</td>
             <td style="width:300px; text-align:left; vertical-align:top">
                 &nbsp;</td>
            <td style="width:30px">&nbsp;</td>
            <td style="width: 300px">
                &nbsp;</td>
           <td style="width: 50px">&nbsp;</td>
        </tr>
    </table>
    
    </asp:Content>

