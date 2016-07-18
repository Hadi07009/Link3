<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTeaskPending.ascx.cs" Inherits="usercontrols_ucTeaskPending" %>
<style type="text/css">
    
    .auto-style2 {
        width: 25px;
    }
     .ucbgcolorfont{       
        background-color:silver;
        font-family:'Helvetica Neue LT';
        font-size:12pt;
        
    }
     .ucColStyle{       
        background-color:#149BF7;
        font-family:'Helvetica Neue LT';
        font-size:12pt;
        text-align:center;
    }
     .ucBorderStyle{       
        border-left-width:0px;
        border-right-width:0px;
        border-bottom:solid;
        border-top:solid;
        border-bottom-width:thin;
        border-top-width:0px;
        border-bottom-color:navy;
        border-top-color:navy;
        background-color:silver;
        font-family:'Helvetica Neue LT';
        
     }

    
</style>
<table style="width: 100%;background-color:white;height:300px" class="ucBorderStyle">
    <tr style="height:25px; font-family:'Helvetica Neue LT'">
        <td class="ucColStyle" colspan="2">
            TASK PENDING</td>        
    </tr>
    <tr style="height:25px; font-family:'Helvetica Neue LT'">
        <td>
            &nbsp;</td>
        <td>
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="false" Font-Size="Small" ForeColor="blue" Font-Underline="true" PostBackUrl="~/modules/HRMS/Approval/frmAttendanceApplicationApproval.aspx"></asp:LinkButton>
        </td>
    </tr>
    <tr style="height:25px; font-family:'Helvetica Neue LT'">
        <td>
            &nbsp;</td>
        <td>
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="false" Font-Size="Small" ForeColor="blue" Font-Underline="true" PostBackUrl="~/modules/HRMS/SelfService/frmAttendanceApplication.aspx"></asp:LinkButton>
        </td>
    </tr>
    <tr style="height:25px; font-family:'Helvetica Neue LT'">
        <td>
            &nbsp;</td>
        <td>
            <asp:LinkButton ID="LinkButton3" runat="server" Font-Bold="false" Font-Size="Small" ForeColor="blue" Font-Underline="true" PostBackUrl="~/modules/HRMS/Details/frmAttendanceIndividualView.aspx"></asp:LinkButton>
        </td>
    </tr>
    <tr style="height:25px; font-family:'Helvetica Neue LT'">
        <td>
            &nbsp;</td>
        <td>
            <asp:LinkButton ID="LinkButton4" runat="server" Font-Bold="false" Font-Size="Small" ForeColor="blue" Font-Underline="true" PostBackUrl="~/modules/HRMS/Details/frmAttendanceIndividualView.aspx"></asp:LinkButton>
        </td>
    </tr>
    <tr style="height:25px; font-family:'Helvetica Neue LT'">
        <td>
            &nbsp;</td>
        <td>
            <asp:LinkButton ID="LinkButton5" runat="server" Font-Bold="false" Font-Size="Small" ForeColor="blue" Font-Underline="true" PostBackUrl="~/modules/HRMS/Details/frmAttendanceIndividualView.aspx"></asp:LinkButton>
        </td>
    </tr>
    <tr style="height:25px; font-family:'Helvetica Neue LT'">
        <td>
            &nbsp;</td>
        <td>
            <asp:LinkButton ID="LinkButton6" runat="server" Font-Bold="false" Font-Size="Small" ForeColor="blue" Font-Underline="true" PostBackUrl="~/modules/HRMS/Details/frmAttendanceIndividualView.aspx"></asp:LinkButton>
        </td>
    </tr>
    <tr style="height:25px; font-family:'Helvetica Neue LT'">
        <td>
            &nbsp;</td>
        <td>
            <asp:LinkButton ID="LinkButton7" runat="server" Font-Bold="false" Font-Size="Small" ForeColor="blue" Font-Underline="true" PostBackUrl="~/modules/HRMS/Details/frmAttendanceIndividualView.aspx"></asp:LinkButton>
        </td>
    </tr>
    
</table>

