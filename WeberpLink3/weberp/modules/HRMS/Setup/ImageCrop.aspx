<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="ImageCrop.aspx.cs" Inherits="modules_HRMS_Setup_ImageCrop" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
                    <ContentTemplate>
                        <script src="../../../js/jquery-1.11.2.min.js"></script>
                        <script src="../../../js/jquery-1.11.2.js"></script>
                        <script src="../../../js/jquery.timepicker.js"></script>
                        <div>
                            
    <asp:Panel ID="pnlUpload" runat="server">
      <asp:FileUpload ID="Upload" runat="server" />
      <br />
      <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
      <asp:Label ID="lblError" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlCrop" runat="server" Visible="false">
      <asp:Image ID="imgCrop" runat="server" />
      <br />
      <asp:HiddenField ID="X" runat="server" />
      <asp:HiddenField ID="Y" runat="server" />
      <asp:HiddenField ID="W" runat="server" />
      <asp:HiddenField ID="H" runat="server" />
      <asp:Button ID="btnCrop" runat="server" Text="Crop" OnClick="btnCrop_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlCropped" runat="server" Visible="false">
      <asp:Image ID="imgCropped" runat="server" Width="16px" />
    </asp:Panel>
  </div>

                    </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                             <%--<asp:PostBackTrigger ControlID="txtEmpId"/> --%>  
                            <%--<asp:PostBackTrigger ControlID="btnShowAttendance"/> 
                                <asp:PostBackTrigger ControlID="btnSubmitAttendance"/> --%>
                             <%--<asp:PostBackTrigger ControlID="btnView"/>  
                                 <asp:PostBackTrigger ControlID="btnSave"/> --%> 
                             </Triggers>
         </asp:UpdatePanel>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery('#imgCrop').Jcrop({
                onSelect: storeCoords
            });
        });

        function storeCoords(c) {
            jQuery('#X').val(c.x);
            jQuery('#Y').val(c.y);
            jQuery('#W').val(c.w);
            jQuery('#H').val(c.h);
        };

</script>
</asp:Content>

