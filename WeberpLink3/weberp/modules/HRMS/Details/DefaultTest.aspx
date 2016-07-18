<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="DefaultTest.aspx.cs" Inherits="modules_HRMS_Details_DefaultTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updpnl2" runat="server">
                    <ContentTemplate>

                    </ContentTemplate>
                        <Triggers>
                             <%--<asp:PostBackTrigger ControlID="txtEmpId"/> --%>  
                        </Triggers>
         </asp:UpdatePanel>
</asp:Content>
