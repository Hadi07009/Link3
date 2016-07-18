<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestRichTextBox2.aspx.cs" Inherits="modules_HRMS_Setup_TestRichTextBox2" ValidateRequest = "false"  %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="RichTextEditor" Namespace="AjaxControls" TagPrefix="cc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function validate() {
            var doc = document.getElementById('Editor1');
            if (doc.contains.length == 0) {
                alert('Please Enter data in text Editor box');
                return false;
            }
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;
                    <FTB:FreeTextBox ID="FreeTextBox1" runat="server" Height="200px" Visible="False" Width="500px"></FTB:FreeTextBox>
                </td>

                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
             <%--<cc1:R ID="Rte1" Theme="Blue" runat="server" />--%>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <cc2:Editor ID="Editor1" runat="server" Height="200px" Width="100%" />
                    
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="return validate()" Text="Save" />
                    
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td id="tdrich" runat="server">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
         
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" Visible="False">
                        <Columns>
                    <asp:TemplateField HeaderText="RichtextBoxData">
                    <ItemTemplate>
                    <asp:Label ID="lbltxt" runat="server" Text='<%#Bind("RichtextData") %>'/>
                    </ItemTemplate>
                    </asp:TemplateField>
                            <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                    </Columns>
                    </asp:GridView>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
