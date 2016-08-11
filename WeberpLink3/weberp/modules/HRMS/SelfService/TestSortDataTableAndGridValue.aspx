<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestSortDataTableAndGridValue.aspx.cs" Inherits="modules_HRMS_SelfService_TestSortDataTableAndGridValue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 87px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 99%;text-align:left">
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label1" runat="server" Text="Employee code"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtEmployeeCode" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtEmployeeName" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                    <asp:Button ID="btnStringFormat" runat="server" OnClick="btnStringFormat_Click" Text="Show String" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="grdEmployeeRecord" runat="server" Width="100%" EmptyDataText="No data found !" OnRowCommand="grdEmployeeRecord_RowCommand" OnRowDeleting="grdEmployeeRecord_RowDeleting">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="10pt" Text="Result"></asp:Label>
                </td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblStringFormat" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
