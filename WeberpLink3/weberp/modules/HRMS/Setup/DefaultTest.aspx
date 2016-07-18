<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="DefaultTest.aspx.cs" Inherits="modules_HRMS_Setup_DefaultTest" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
                    <ContentTemplate>

                    </ContentTemplate>
                        <Triggers>
                             <%--<asp:PostBackTrigger ControlID="txtEmpId"/> --%>  
                            <%--<asp:PostBackTrigger ControlID="btnShowAttendance"/> 
                                <asp:PostBackTrigger ControlID="btnSubmitAttendance"/> --%>
                             <%--<asp:PostBackTrigger ControlID="btnView"/>  
                                 <asp:PostBackTrigger ControlID="btnSave"/> --%> 
                             </Triggers>
         </asp:UpdatePanel>
</asp:Content>

