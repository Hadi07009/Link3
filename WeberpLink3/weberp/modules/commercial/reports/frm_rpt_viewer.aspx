<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_rpt_viewer.aspx.cs" Inherits="frm_rpt_viewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SSCML Reports</title>
    <script type="text/javascript">
        function openPopup() {
            
            this.window.focus();
            return false;
        }
</script>

</head>
<body onload="openPopup()">
    <form id="form1" runat="server"  >
    <div>
    <asp:UpdatePanel ID="pnl" runat="server">
    <ContentTemplate>
    
        <div style="text-align: center">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" Height="940px" HyperlinkTarget="_blank"
                ReuseParameterValuesOnRefresh="True" ToolPanelView="None" Width="1210px" 
                PrintMode="ActiveX"  GroupTreeImagesFolderUrl="" 
                onunload="CrystalReportViewer1_Unload" ToolbarImagesFolderUrl="" 
                ToolPanelWidth="200px"  />
            
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>

    </div>
    </form>
</body>
</html>
